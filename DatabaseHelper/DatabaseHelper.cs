using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.EntityClient;
using System.Diagnostics;
using System.IO;
//using System.Data.SqlServerCe;
//using DatabaseHelper.Properties.DataSources.SemanticDBDataSetTableAdapters;

namespace DatabaseHelper
{
    /// <summary>
    /// Класс DatabaseHelper предназначен для непосредственной
    /// работы с базой данных
    /// </summary>
    public class DatabaseHelper
    {
        SemanticDBEntities m_Database = null;
        bool m_bMainDB = false;

        public DatabaseHelper(string sConnectionString)
        {
            try
            {
                ConnectionStringSettings conn = ConfigurationManager.ConnectionStrings["SemanticDBEntities"];
                m_Database = new SemanticDBEntities();

                string s = m_Database.Connection.ConnectionString;
            }
            catch (Exception ex)
            {
                Trace.Listeners.Add(new TextWriterTraceListener(Path.Combine(Directory.GetCurrentDirectory(), "log.txt")));
                Trace.AutoFlush = true;
                Trace.WriteLine(ex.Message);
                
                int j = 1;
            }

        }

        bool MainDB
        {
            get { return m_bMainDB; }
            set { m_bMainDB = value; }
        }

        /// <summary>
        /// Возвращает уникальный идентификатор из БД
        /// </summary>
        /// <param name="sWord">слово, для которого надо вернуть идентификатор</param>
        /// <returns>идентификатор БД</returns>
        private int GetWordId(string sWord)
        {

            var parentId = from p in m_Database.tbl_WordItems
                           where p.Word == sWord
                           select p.Id;

            return parentId.Count() > 0 ? parentId.First() : -1;
        }

        /// <summary>
        /// Возвращает объект сущность слова из БД
        /// </summary>
        /// <param name="sWord">слово, для которого надо вернуть сущность из БД</param>
        /// <returns>сущность БД из таблицы tbl_Words или null, если слово не найдено</returns>
        public WordItem GetWord(string sWord)
        {
            var words = from p in m_Database.tbl_WordItems
                           where p.Word == sWord
                           select p;
            return words.Count() > 0 ? words.First() : null;
        }

        /// <summary>
        /// Возвращает сущность "слово" из БД
        /// </summary>
        /// <param name="nId">идентификатор слова в БД</param>
        /// <returns>сущность БД из таблицы tbl_Words или null, если идентификатор не найден</returns>
        public WordItem GetWordById(int nId)
        {

            var words = from p in m_Database.tbl_WordItems
                        where p.Id == nId
                        select p;

            return words.Count() > 0 ? words.First() : null;
        }

        //items should be unique
        /// <summary>
        /// Возвращает сущность "связь" из БД
        /// </summary>
        /// <param name="sParent">слово-родитель</param>
        /// <param name="sChild">слово-потомок</param>
        /// <returns>сущность БД из таблицы tbl_Relations или null, если пара sParent-sChild не найдена</returns>
        public RelationItem GetRelationItem(string sParent, string sChild)
        {
            int nParentId = GetWordId(sParent);
            if (nParentId < 0)
            {
                return null;
            }

            int nChildId = GetWordId(sChild);
            if (nChildId < 0)
            {
                return null;
            }

            var rel = from p in m_Database.tbl_RelationItems
                      where p.ChildId == nChildId && p.ParentId == nParentId
                      select p;

            return rel.Count() > 0 ? rel.First() : null;
        }

        //items should be unique
        /// <summary>
        /// Возвращает сущность "связь" из БД
        /// </summary>
        /// <param name="nParent">идентификатор слова-родителя</param>
        /// <param name="nChild">идентификатор слова-потомка</param>
        /// <returns>сущность БД из таблицы tbl_Relations или null, если пара nParent-nChild не найдена</returns>
        private RelationItem GetRelationItem(int nParent, int nChild)
        {
           var rel = from p in m_Database.tbl_RelationItems
                     where p.ChildId == nChild && p.ParentId == nParent
                     select p;

            return rel.Count() > 0 ? rel.First() : null;
        }

        /// <summary>
        /// проверяет есть ли сущность "связь" и добавляет ее в БД если отсутствует
        /// </summary>
        /// <param name="sParent">слово-родитель</param>
        /// <param name="sChild">слово-потомок</param>
        /// <param name="relType">тип отношения</param>
        /// <param name="relGroupId">номер группы отношения. 
        /// Этот номер используется для определения исходной формы слова, т.к. словарь словоформ может содержать несколько исходных значений</param>
        /// <returns>созданную сущность "связь"</returns>
        public RelationItem AddRelation(string sParent, string sChild, RelationType relType, short? relGroupId)
        {
            int nParentId = GetWordId(sParent);
            if (nParentId < 0)
            {
                nParentId = AddWord(sParent, true).Id;
            }

            int nChildId =  GetWordId(sChild);
            if (nChildId < 0)
            {
                nChildId = AddWord(sChild, true).Id;
            }

            RelationItem anItem = GetRelationItem(nParentId, nChildId);
            if (anItem == null)
            {
                anItem = RelationItem.CreateRelationItem(nParentId, nChildId, true, (short)relType, 0);
                anItem.RelationTypeGroup = relGroupId;

                m_Database.AddTotbl_RelationItems(anItem);
                m_Database.SaveChanges();
            }

            return anItem;
        }

        
        /// <summary>
        /// устаревшаяя, использовалась до перевода на SQL CE 4.0
        /// Create real function or change database
        /// </summary>
        /// <returns></returns>
        private int CreateUniqueID()
        {
            int n = m_Database.tbl_WordItems.Count() + 1;
            return n;
        }

        /// <summary>
        /// проверяет есть ли сущность "слово" и добавляет ее в БД если отсутствует
        /// </summary>
        /// <param name="sWord">слово, которое надо создать</param>
        /// <param name="bSave">флаг, показываующий надо ли сохранять слово. Вероятно стоит убрать этот параметр.</param>
        /// <returns>созданную сущность "слово"</returns>
        public WordItem AddWord(string sWord, bool bSave)
        {
            WordItem aWord = GetWord(sWord);
            if (aWord == null)
            {
                aWord = WordItem.CreateWordItem(0 + (bSave ? 1 : 0), sWord, (short)WordStatus.None, MainDB);

                if (bSave)
                {
                    m_Database.AddObject("tbl_WordItems", aWord);
                    m_Database.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);
                }
            }
            return aWord;
        }

        /// <summary>
        /// удаляет слово из БД
        /// </summary>
        /// <param name="sWord">слово для удалния</param>
        /// <returns>объект сущности "слово" удаленный из БД или null, если слово отсутствует</returns>
        public WordItem RemoveWord(string sWord)
        {
            WordItem aWord = GetWord(sWord);
            if (aWord != null)
            {
               m_Database.DeleteObject(aWord);
               m_Database.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);
            }
            return aWord;
        }

        /// <summary>
        /// меняет параметры сущности "слово" в БД
        /// </summary>
        /// <param name="sWord">слово для которого делаются изменения</param>
        /// <param name="newStatus">новый статус слова</param>
        /// <param name="nOmon">номер омонима, соответствует омониму из толкового словаря</param>
        /// <param name="bReviewed">флаг, показываующий редактировали ли уже найденную связь</param>
        /// <returns>изменную сущность "слово"</returns>
        public WordItem SetStatus(string sWord, WordStatus newStatus, short? nOmon, bool bReviewed)//string sWord, WordStatus eStatus)
        {
            WordItem anItem = AddWord(sWord, true);

            if (anItem != null)
            {
                anItem.Status = (short)newStatus;
                anItem.StatusLastTimeUpdate = DateTime.UtcNow;
                anItem.Homonym = nOmon;
                anItem.Reviewed = bReviewed;
                m_Database.SaveChanges();
            }

            return anItem;
        }

        /// <summary>
        /// возвращает статус для слова, которое уже находится в БД
        /// </summary>
        /// <param name="sWord">слово, которое уже находится в БД</param>
        /// <returns>статус слова</returns>
        public WordStatus GetStatus(string sWord)
        {
            return (WordStatus)GetWord(sWord).Status;
        }

        /// <summary>
        /// возвращает список сущностей "связь", в которых словом-родителем являетс входной параметр
        /// </summary>
        /// <param name="sWord">слово-родитель, для которого надо найти слова потомки</param>
        /// <returns>список сущностей "связь" или null, если слово-родитель отсутсвует</returns>
        public List<RelationItem> GetChildren(string sWord)
        {
            int nParentId = GetWordId(sWord);
            if (nParentId < 0)
            {
                return null;// nParentId = AddWord(sWord, true).Id;
            }

            var relations = from rel in m_Database.tbl_RelationItems
                            where rel.ParentId == nParentId
                            select rel;
            return relations.Count() > 0 ? relations.ToList() : null;
        }

        /// <summary>
        /// возвращает список сущностей "связь", в которых словом-потомок являетс входной параметр
        /// </summary>
        /// <param name="sWord">слово-потомок, для которого надо найти слова родителей</param>
        /// <returns>список сущностей "связь" или null, если слово-потомок отсутсвует</returns>
        public List<RelationItem> GetParents(string sWord)
        {
            int nChildId = GetWordId(sWord);
            if (nChildId < 0)
            {
                return null;//nChildId = AddWord(sWord, true).Id;
            }

            var relations = from rel in m_Database.tbl_RelationItems
                            where rel.ChildId == nChildId
                            select rel;
            return relations.Count() > 0 ? relations.ToList() : null;
        }

        /// <summary>
        /// использовалось для тестов, пока не нужна
        /// </summary>
        /// <param name="sWord"></param>
        public void UpdateWord(string sWord)
        {

            var Word = from word in m_Database.tbl_WordItems
                       where sWord == word.Word
                       select word;
            
            WordItem anItem = Word.First();
            if (anItem != null)
            {
                
            }
        }

        /// <summary>
        /// использовалась для тестов, пока не нужна
        /// </summary>
        /// <param name="sWord"></param>
        /// <param name="sChild"></param>
        public void UpdateRelation(string sWord, string sChild)
        {
            var wordId = from p in m_Database.tbl_WordItems
                         where p.Word == sWord
                         select new
                         {
                             Id = p.Id
                         };

            var childId = from p in m_Database.tbl_WordItems
                          where p.Word == sWord
                          select new
                          {
                              Id = p.Id
                          };

            var rel = from p in m_Database.tbl_RelationItems
                      where p.ChildId == childId.First().Id && p.ParentId == wordId.First().Id
                      select p;

            if (rel.Count() > 0)
            {
                rel.First().RelationType = 1;
            }
            else
            {
                RelationItem aRelation = RelationItem.CreateRelationItem(1, 2, true, (short)RelationType.Normal, 0);
                m_Database.AddTotbl_RelationItems(aRelation);
            }

            m_Database.SaveChanges();            
        }

        /// <summary>
        /// сохраняет измененную сущность "связь" в БД
        /// </summary>
        /// <param name="anItem">сущность связь, которую надо сохранить</param>
        public void UpdateRelation(RelationItem anItem)
        {
            var rel = from relation in m_Database.tbl_RelationItems
                      where anItem.Id == relation.Id
                      select relation;
            if (rel.Count() > 0)
            {
                rel.First().RelationType = anItem.RelationType;
                m_Database.SaveChanges();
            }                           
        }
    }

    /// <summary>
    /// статус слова
    /// </summary>
    public enum WordStatus
    {
        /// <summary>
        /// слово не обрабытывалось
        /// </summary>
        None = 0,

        /// <summary>
        /// обработка в прогрессе
        /// </summary>
        InProgress,

        /// <summary>
        /// обработка завершена
        /// </summary>
        Completed
    }

    // тип связи
    public enum RelationType
    {
        /// <summary>
        /// обчная связь
        /// </summary>
        Normal = 0,

        /// <summary>
        /// игнорируемая связь
        /// </summary>
        Ignored,

        /// <summary>
        /// неопределнная связь, используется 
        /// когда слово имеет несколько разных исходных форм и 
        /// нельзя определить подходящее
        /// </summary>
        Tentative,

        /// <summary>
        /// слово было в словаре словоформ, но отсутствует в толковом словаре
        /// </summary>
        Absent
    }
}
