using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SemanticLinkHelper.ExplDictService;
using SemanticLinkHelper.WordFormsService;
using MessageCompressor;
using DatabaseHelper;
using ExplEntities;
using System.Net;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text.RegularExpressions;
using System.Windows;
using System.Globalization;
using SemanticLinkHelper.CommonDictService;
using System.Collections.ObjectModel;


namespace SemanticLinkHelper
{
    //possible may be other ids
    public enum Langparts
    {
        Verb = -13,
        Noun = -1,
        Noun2 = 1,
        Noun3 = 21
        
    }

    public class SemanticHelper
    {

        DatabaseHelper.DatabaseHelper m_dbHelper = new DatabaseHelper.DatabaseHelper("");
        ChannelFactory<IExplDic> explFactory = new ChannelFactory<IExplDic>("ExplHttpEndpoint");
        ChannelFactory<ICommonDic> commonFactory = new ChannelFactory<ICommonDic>("WSHttpBinding_ICommonDic");

        ClientCredentials m_Credentials = null;

        ChannelFactory<Ilib5> wordFormsFactory = new ChannelFactory<Ilib5>("lib5httpEndpoint");

        Dictionary<int, string> m_Editors = null;
        int m_LsId = 1;
        const int ROWS_PER_PAGE = 100;

        /// <summary>
        /// Конструктор
        /// </summary>
        public SemanticHelper()
        {
            explFactory.Endpoint.Behaviors.Add(new MessageCompressionAttribute(Compress.Reply | Compress.Request));

            //last few weeks I started to get exception about certificate validation
            explFactory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            commonFactory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
        }              

        /// <summary>
        /// Создает канал для вебсервиса толкового словаря
        /// </summary>
        /// <returns>канал вебсервиса толкового словаря</returns>
        internal IExplDic CreateChannel()
        {
            IExplDic explService = explFactory.CreateChannel();
            ((IClientChannel)explService).Open();
            return explService;
        }

        /// <summary>
        /// Создает канал для вебсервиса словаря словоформ
        /// </summary>
        /// <returns>канал вебсервиса словаря словоформ</returns>
        internal Ilib5 CreateWordFormsChannel()
        {
            Ilib5 wordFormsService = wordFormsFactory.CreateChannel();
            ((IClientChannel)wordFormsService).Open();
            return wordFormsService;
        }

        /// <summary>
        /// Создает канал для вебсервиса учета пользователей
        /// </summary>
        /// <returns>канал вебсервиса учета пользователей</returns>
        internal ICommonDic CreateCommonChannel()
        {
            ICommonDic commonService = commonFactory.CreateChannel();
            ((IClientChannel)commonService).Open();
            return commonService;
        }

        /// <summary>
        /// Высталяет данные пользователя для толкового словаря
        /// </summary>
        /// <param name="credentials">логин/пароль пользователя</param>
        public void SetCredentials(ClientCredentials credentials)
        {
            m_Credentials = credentials;

            var credentialBehaviour = explFactory.Endpoint.Behaviors.Find<ClientCredentials>();
            credentialBehaviour.UserName.UserName = m_Credentials.UserName.UserName;
            credentialBehaviour.UserName.Password = m_Credentials.UserName.Password;
        }

        /// <summary>
        /// Высталяет данные пользователя для вебсервиса учета пользователей
        /// </summary>
        /// <param name="credentials">логин/пароль пользователя</param>
        public void SetCredentials2(ClientCredentials credentials)
        {
            m_Credentials = credentials;

            var credentialBehaviour = commonFactory.Endpoint.Behaviors.Find<ClientCredentials>();
            credentialBehaviour.UserName.UserName = m_Credentials.UserName.UserName;
            credentialBehaviour.UserName.Password = m_Credentials.UserName.Password;


        }

        /// <summary>
        /// Возвращает семнатическую ветвь. Слово и все его непосреджственные потомки из словарной статьи.
        /// </summary>
        /// <param name="sWord">слово родитель</param>
        /// <param name="SearchOption">настройки поиска</param>
        /// <returns>объект семантической ветви</returns>
        public WordSemanticBranch GetSemanticLink(string sWord, Options SearchOption)
        {

            WordSemanticBranch lnk = null;

            IExplDic curChannel = null;
            try
            {
                lnk = new WordSemanticBranch();
                lock (m_dbHelper)
                {
                    WordItem anWord = m_dbHelper.GetWord(sWord);
                    if (anWord == null)
                    {
                        anWord = m_dbHelper.AddWord(sWord, true);
                    }
                    lnk.ParentWord = new RegistryWord(anWord);
               
                }

                curChannel = CreateChannel();

                ExplDictService.nom[] articles = curChannel.ExplGetArticles(sWord, false, false, false, m_LsId);

                foreach (ExplDictService.nom article in articles)
                {
                    if ( ShouldSkip(article.part) )//существительное
                        continue;

                    foreach (ExplDictService.formula Value in article.formula)
                    {
                        if (Value.kind == 0 && //смысл
                            Value.lv == 2) //толкование
                        {
                            string text = Value.interpr;
                            string[] words = text.Split(new char[] { ' ', ',', '.', '!', '?', ';', ':' });
                            short nGroup = 0;
                            foreach (string word in words)
                            {
                                lnk.Children.AddRange(GetChildren(sWord, word, curChannel, nGroup));
                                nGroup++;
                            }
                        }
                    }
                }
                ((IClientChannel)curChannel).Close();
            }
            catch (Exception ex)
            {
                if (curChannel != null)
                    ((IClientChannel)curChannel).Abort();
                MessageBox.Show(string.Format("{0} {1}", "GetSemanticLink", ex.Message));
            }

            return lnk;
        }

        /// <summary>
        /// проверяет надо ли пропустить искомое слово. На данный момент времени проверяет, является ли оно существительным
        /// </summary>
        /// <param name="nLangPart">идентификатор части речи из толкового словаря</param>
        /// <returns>true, если слово не существительное</returns>
        bool ShouldSkip(int nLangPart)
        {
            return !(nLangPart == (int)Langparts.Noun || nLangPart == (int)Langparts.Noun2 || nLangPart == (int)Langparts.Noun3);
        }

        /// <summary>
        /// находит все исходные формы слова
        /// </summary>
        /// <param name="sParent">слово родитель</param>
        /// <param name="sAnyFormWord">слово из правой части словарной статьи</param>
        /// <param name="explChannel">канал толкового словаря</param>
        /// <param name="relGroupId">номер группы отношений. используется если одно слово имеет несколько равнозначных исходных форм</param>
        /// <returns>список потомков с снайденным типом связи</returns>
        List<WordLink> GetChildren(string sParent, string sAnyFormWord, IExplDic explChannel, short relGroupId)
        {
            Ilib5 wordFormsChannel = null;
            List<WordLink> listChildren = null;

            try
            {
                listChildren = new List<WordLink>();
                if (sAnyFormWord.Length == 0)
                    return listChildren;

                wordFormsChannel = CreateWordFormsChannel();

                WordUniAndPartAndOmon[] forms = wordFormsChannel.getAllFirstWFormsWithParts(sAnyFormWord);

                foreach (WordUniAndPartAndOmon form in forms)
                {
                    string s = form.part;
                    string sOriginalForm = form.forma;
                    if (IsNoun(form.part))
                    {
                        sOriginalForm = RemoveAccents(sOriginalForm);
                        if (sOriginalForm.Length > 0)
                        {
                            //can this be equal to zero?
                            int nVal = explChannel.ExplCheckReestr(sOriginalForm, form.omon, m_LsId);
                            if (nVal < 0 && form.omon != 0)
                            {
                                nVal = explChannel.ExplCheckReestr(sOriginalForm, 0, m_LsId);
                                sOriginalForm = RegistryWord.CreateNameWithHomonym(sOriginalForm, string.Empty);
                            }
                            else
                            {
                                sOriginalForm = RegistryWord.CreateNameWithHomonym(sOriginalForm, form.omon == 0 ? string.Empty : form.omon.ToString());
                            }

                            lock (m_dbHelper)
                            {
                                RelationItem newItem = m_dbHelper.GetRelationItem(sParent, sOriginalForm);
                                if (newItem == null)
                                {
                                    RelationType relType = (nVal < 0) ? RelationType.Absent : (listChildren.Count > 0 ? RelationType.Tentative : RelationType.Normal);
                                    newItem = m_dbHelper.AddRelation(sParent, sOriginalForm, relType, relGroupId);
                                }
                                WordItem aWord = m_dbHelper.GetWord(sOriginalForm);
                                WordLink aLink = new WordLink(newItem, aWord);
                                if( !listChildren.Contains(aLink) )
                                    listChildren.Add(aLink);
                            }
                        }
                    }
                }

                ((IClientChannel)wordFormsChannel).Close();
            }
            catch (Exception ex)
            {
                if (wordFormsChannel != null)
                    ((IClientChannel)wordFormsChannel).Abort();
                MessageBox.Show(string.Format("{0} {1}", "GetChildren", ex.Message));
            }

            return listChildren;
        }


        /// <summary>
        /// Возвращает семнатическую ветвь из локально БД. Слово и все его непосреджственные потомки.
        /// </summary>
        /// <param name="sWord">слово родитель</param>
        /// <returns>объект семантической ветви</returns>
        public WordSemanticBranch GetSemanticLinkFromDB(string sWord)
        {
            List<RelationItem> children = null;
            WordSemanticBranch link = null;
            lock (m_dbHelper)
            {
                children = m_dbHelper.GetChildren(sWord);
                WordItem wordItem = m_dbHelper.GetWord(sWord);
                if (wordItem == null)
                {
                    wordItem = m_dbHelper.AddWord(sWord, true);
                }

                link = new WordSemanticBranch();
                link.ParentWord = new RegistryWord(wordItem);
                
                if (children != null)
                {
                    foreach (RelationItem anItem in children)
                    {
                        link.Children.Add(new WordLink(anItem, m_dbHelper.GetWordById(anItem.ChildId)));
                    }
                }
            }

            return link;
        }

        /// <summary>
        /// проверят логин/пароль пользователя путем запроса веб-сервису толкового словаря
        /// </summary>
        /// <returns>true, если пользователь найден и имеет доступ к словарю</returns>
        public bool CheckCredentials()
        {
            IExplDic curChannel = null;
            try
            {
                curChannel = CreateChannel();
                string[] roles = curChannel.GetUserRoles(m_LsId);
                ((IClientChannel)curChannel).Close();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
                if (curChannel != null)
                    ((IClientChannel)curChannel).Abort();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Определяет является ли слово существительным по идентификатору словаря словоформ
        /// </summary>
        /// <param name="btLangPartId">идентификатору части речи из словаря словоформ</param>
        /// <returns>true, если слово существительное</returns>
        bool IsNoun(byte btLangPartId)
        {
            return btLangPartId == 7 || btLangPartId == 5;
        }

        /// <summary>
        /// Определяет является ли слово существительным по строковому описанию из словаря словоформ
        /// </summary>
        /// <param name="sLangDesc">строковое описание части речи</param>
        /// <returns>true, если слово существительное</returns>
        bool IsNoun(string sLangDesc)
        {
            return sLangDesc.IndexOf("іменник", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Заменяет знаки ударения на знак # словах найденных в словаре словоформ
        /// </summary>
        /// <param name="sOrig">слово из словаря словоформ</param>
        /// <returns>измененное слово, с замененными знаками ударения</returns>
        string RemoveAccents(string sOrig)
        {
            //string s = "(а́|ю́|і́|и́|я́|у́|е́|ї́|о́)";

            StringBuilder sb = new StringBuilder(sOrig.ToLower());
            sb.Replace((char)769, '#');

            string sVowels = "уеїіаоєяию";
            string sOut = sb.ToString();
            if (!sOut.Contains('#'))
            {
                int nSum = sOut.Sum(aChar => sVowels.Contains(aChar) ? 1 : 0);
                if (nSum > 1)
                    return string.Empty;
            }

            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// обновляет статус слова в БД
        /// </summary>
        /// <param name="aWord">слово из БД</param>
        /// <param name="eStatus">новый статус</param>
        /// <returns>слов с обновленным статусом</returns>
        public RegistryWord UpdateWordStatus(RegistryWord aWord, WordStatus eStatus)
        {
            lock(m_dbHelper)
            {
                aWord.Status = eStatus;
                return new RegistryWord(m_dbHelper.SetStatus(aWord.Word, (DatabaseHelper.WordStatus)eStatus, aWord.Homonym, aWord.Reviewed));
            }
        }

        /// <summary>
        /// Обновляет коллекцию семантических ветвей после запроса списка слов из толкового словаря
        /// </summary>
        /// <param name="nLowerBound">нижняя граница слов, которые надо получить из толкового словаря</param>
        /// <param name="collection" out>коллекция которую надо обновить информацией их толкового словаря</param>
        public void AddDictionaryWords(int nLowerBound, ObservableCollection<WordSemanticBranch> collection)
        {
            //for debug purposes
            //return;
            IExplDic aDic = null;
            try
            {

                aDic = CreateChannel();
                //int nMaxRows = aDic.GetRowCount(string.Empty, false, null, false, m_LsId);
                int nRequestLowerBound = nLowerBound - ROWS_PER_PAGE / 2;
                if (nRequestLowerBound < 1)
                    nRequestLowerBound = 1;
                ExplDictService.elList[] page = aDic.SupplyPageOfData(nRequestLowerBound, ROWS_PER_PAGE, true, string.Empty, false, null, false, m_LsId);
                foreach (ExplDictService.elList elem in page)
                {
                    WordSemanticBranch newBranch = GetSemanticLinkFromDB(RegistryWord.CreateNameWithHomonym(elem.word, elem.omon));
                    if (newBranch == null)
                    {
                        newBranch = new WordSemanticBranch();
                        newBranch.ParentWord = new RegistryWord(elem);
                    }

                    int nIndex = collection.IndexOf(newBranch);
                    if (nIndex < 0)
                    {
                        collection.Add(newBranch);
                    }
                    else
                    {
                        collection[nIndex] = newBranch;
                    }
                }
                ((IClientChannel)aDic).Close();
            }
            catch (Exception ex)
            {
                if (aDic != null)
                    ((IClientChannel)aDic).Abort();
                MessageBox.Show(string.Format("{0} {1}", "AddDictionaryWords", ex.Message));
            }
        }

        /// <summary>
        /// добавляет связи в БД для данной семантической ветви
        /// </summary>
        /// <param name="branch">семантическая ветвь, которую надо добавить в базу данных</param>
        public void AddDictionaryWord(WordSemanticBranch branch)
        {
            lock (m_dbHelper)
            {
                m_dbHelper.SetStatus(branch.ParentWord.Word, (DatabaseHelper.WordStatus)branch.ParentWord.Status, branch.ParentWord.Homonym, branch.ParentWord.Reviewed);
                foreach (WordLink link in branch.Children)
                {
                    m_dbHelper.AddRelation(branch.ParentWord.Word, link.Child.Word, (RelationType)link.RelationType, link.RelationGroupId);
                }
            }
        }

        /// <summary>
        /// осуществляет поиск слова в словаре и загружает ближащие слова
        /// </summary>
        /// <param name="sWord">слово для поиска</param>
        /// <param name="collection">коллекция которую надо обновить информацией их толкового словаря</param>
        /// <returns>номер нижнего слова из диапазона запрашиваемых слов</returns>
        public int GoToWord(string sWord, ObservableCollection<WordSemanticBranch> collection)
        {
            IExplDic aDic = null;
            int nRequestLowerBound = 1;
            try
            {
                aDic = CreateChannel();
                int nMaxRows = 0;
                int nLowerBound = aDic.Search(out nMaxRows, sWord, true, string.Empty, false, null, false, m_LsId);
                nRequestLowerBound = nLowerBound - ROWS_PER_PAGE / 2;
                if (nRequestLowerBound < 1)
                    nRequestLowerBound = 1;

                AddDictionaryWords(nLowerBound, collection);

                ((IClientChannel)aDic).Close();
            }
            catch (Exception ex)
            {
                if (aDic != null)
                    ((IClientChannel)aDic).Abort();
                MessageBox.Show(string.Format("{0} {1}", "GoToWord", ex.Message));
            }

            return nRequestLowerBound;
        }

        /// <summary>
        /// обновляет или добавляет связи в БД для данной семантической ветви
        /// </summary>
        /// <param name="aLink">семантическая ветвь, данные о которой надо добавить или обновить</param>
        public void UpdateRelations(WordSemanticBranch aLink)
        {
            foreach(WordLink link in aLink.Children)
            {
                RelationItem anItem = m_dbHelper.GetRelationItem(aLink.ParentWord.Word, link.Child.Word);
                if (anItem == null)
                {
                    m_dbHelper.AddRelation(aLink.ParentWord.Word, link.Child.Word, (RelationType)link.RelationType, link.RelationGroupId);
                }
                else
                {
                    anItem.RelationType = (short)link.RelationType;
                    anItem.RelationTypeGroup = link.RelationGroupId;
                    m_dbHelper.UpdateRelation(anItem);
                }
                
            }
        }
    }
}
