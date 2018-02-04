using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using HyperChain.HyperChainSelfHostedService;
using System.Threading;
//using HyperChain.Properties.DataSources;
//using HyperChain.Properties.DataSources.SemanticDBDataSetTableAdapters;
using System.Data;
//using System.Data.SqlClient;
//using System.Data.SqlServerCe;
//using DatabaseHelper;
using SemanticLinkHelper;
using System.ServiceModel.Description;
using System.Collections.ObjectModel;
using System.Windows;


namespace HyperChain
{
    public class ExplProcessedEventArgs : EventArgs
    {
        public int MaxExpl;
        public int CurExpl;
        public SearchRun CurrentRun;
        public bool Update = false;
  
        public ExplProcessedEventArgs( int nCur, int nMax )
        {
            MaxExpl = nMax;
            CurExpl = nCur;
        }
        public ExplProcessedEventArgs(SearchRun aRun, bool bUpdate)
        {
            CurrentRun = aRun;
            Update = bUpdate;
        }
    }

    public class HyperChainAddedEventArgs : EventArgs
    {

        public ShowHyperRun CurrentRun { get; set; }

        public HyperChainAddedEventArgs(ShowHyperRun aRun)
        {
            CurrentRun = aRun;
        }
    }

    public class ClientLogic
    {
        // A delegate type for hooking up change notifications.
        public delegate void ExplProcessedEventHandler(object sender, ExplProcessedEventArgs e);
        public delegate void HyperChainAddedEventHandler(object sender, HyperChainAddedEventArgs e);

        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public event ExplProcessedEventHandler ExplProcessed;
        public event HyperChainAddedEventHandler HyperChainAdded;

        
        string m_sWordToSearch;
        SemanticHelper m_SemanticHelper = new SemanticHelper();

        string m_sUser = string.Empty;
        string m_sPassword = string.Empty;

        //ObservableDictionary<string, WordSemanticBranch> m_ListLinks = new ObservableDictionary<string, WordSemanticBranch>();
        
        //Dictionary<string, WordSemanticBranch> m_ListLinks = new Dictionary<string, WordSemanticBranch>();

        //TimeSpan m_Delay = new TimeSpan(0, 20, 0);
        //public Dictionary<string, WordSemanticBranch> ListLinks
        //{
        //    get{ return m_ListLinks; }
        //}
        //ObservableCollection<RegistryWord> m_ListWords = new ObservableCollection<RegistryWord>();
        ObservableCollection<WordSemanticBranch> m_ListWords = new ObservableCollection<WordSemanticBranch>();
        ObservableCollection<SearchRun> m_ListRuns = new ObservableCollection<SearchRun>();

        public long StopCreateHyper = 0;

        public ObservableCollection<WordSemanticBranch> ListWords
        {
            get { return m_ListWords; }
            set { m_ListWords = value; }
        }

        public ObservableCollection<SearchRun> ListRuns
        {
            get { return m_ListRuns; }
            set { m_ListRuns = value; }
        }

        public ClientLogic()
        {
            
            //ClientCredentials user = new ClientCredentials();
            //user.UserName.UserName = m_sUser;
            //user.UserName.Password = m_sPassword;

            //m_SemanticHelper.SetCredentials(user);                
        }

        public void LoadPage(int nLowerBound)
        {
             m_SemanticHelper.AddDictionaryWords(nLowerBound, m_ListWords);
        }

        public bool Login(string sUser, string sPassword)
        {
            m_sUser = sUser;
            m_sPassword = sPassword;

            ClientCredentials user = new ClientCredentials();
            user.UserName.UserName = m_sUser;
            user.UserName.Password = m_sPassword;

            m_SemanticHelper.SetCredentials(user);
            return m_SemanticHelper.CheckCredentials();
        }

        public void FillPage(int nNum)
        {
            try
            {
               // HyperChainServiceClient client = new HyperChainServiceClient();
              //  List<string> m_Words = client.GetPageInfo(nNum);
                int q = 1;
            }
            catch (Exception ex)
            {

            }
        }

        public void GoToWord(string sWord)
        {
            m_SemanticHelper.GoToWord(sWord, m_ListWords);
        }

        private void OnExplProcessed(ExplProcessedEventArgs e)
        {
            if (ExplProcessed != null)
            {
                ExplProcessed(this, e);
            }
        }

        private void OnHyperChainAdded(HyperChainAddedEventArgs e)
        {
            if (HyperChainAdded != null)
            {
                HyperChainAdded(this, e);
            }
        }

        public void StartSearch(object searchRun)
        {
            try
            {
                SearchRun curRun = (SearchRun)searchRun;

                //if (!curRun.ForceSearch)
                //{
                //    lnk = m_SemanticHelper. GetSemanticLinkFromDB(sWord);

                //    if (lnk != null)
                //    {
                //        lock (m_dbHelper)
                //        {

                //            //lnk.Granny = new RegistryWord(m_dbHelper.GetWord(sParent));
                //        }
                //        if (lnk.ParentWord.Status != WordStatus.eNotProcessed)
                //            return lnk;
                //        else
                //            UpdateWordStatus(lnk.ParentWord, WordStatus.eInProgress);
                //    }
                //}

                //m_SemanticHelper.UpdateWordStatus(curRun.WordLink.ParentWord, WordStatus.eNotProcessed);//WordStatus.eInProgress);
                //OnExplProcessed(new ExplProcessedEventArgs(curRun, false));
                if (curRun.DeepSearch <= 0)
                {
                    //if( curRun.WordLink.ParentWord.Status 
                    //m_SemanticHelper.UpdateWordStatus(curRun.WordLink.ParentWord, WordStatus.eNotProcessed);
                    //OnExplProcessed(new ExplProcessedEventArgs(curRun, true));

                }
                else
                {
                    Search(curRun);
                    m_SemanticHelper.UpdateWordStatus(curRun.WordLink.ParentWord, WordStatus.eCompleted);
                    OnExplProcessed(new ExplProcessedEventArgs(curRun, false));
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}\n{1}", "CLientLogic.StartSearch", ex.Message));
            }
        }

        public void StartCreateHyper(object showRun)
        {
            try
            {
                ShowHyperRun curRun = (ShowHyperRun)showRun;
                if (curRun.CurDeep > curRun.DeepSearch || Interlocked.Read(ref StopCreateHyper) == 0 )
                    return;

                //WordSemanticBranch newBranch = m_SemanticHelper.GetSemanticLinkFromDB(curRun.CurDeep == 1 ? "тест2" : curRun.WordLink.ParentWord.Word);
                WordSemanticBranch newBranch = m_SemanticHelper.GetSemanticLinkFromDB(curRun.WordLink.ParentWord.Word);
                curRun.WordLink = newBranch;
                OnHyperChainAdded(new HyperChainAddedEventArgs(curRun));
                int nNewDeepSearch = curRun.DeepSearch - 1;
                bool bNewBranch = false;
                foreach (WordLink link in newBranch.Children)
                {
                    WordSemanticBranch childBranch = m_SemanticHelper.GetSemanticLinkFromDB(link.Child.Word);
                    if (childBranch != null && 
                        (link.RelationType == WordLinkType.eNormal || 
                        link.RelationType == WordLinkType.eTentative) )
                    {
                        StartCreateHyper(new ShowHyperRun(childBranch, curRun.DeepSearch, curRun.CurDeep + 1, bNewBranch));
                    }
                    bNewBranch = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}\n{1}", "CLientLogic.StartSearch", ex.Message));
            }
        }

        public void Search(SearchRun searchRun)
        {
            try
            {
               

                WordSemanticBranch lnk = null;

                if (!searchRun.ForceSearch)
                {
                    lnk = m_SemanticHelper.GetSemanticLinkFromDB(searchRun.WordLink.ParentWord.Word);

                    if (lnk != null)
                    {
                        if (lnk.ParentWord.Status == WordStatus.eNotProcessed)
                            lnk = null;
                    }
                }

                if (lnk == null)
                {
                    m_SemanticHelper.UpdateWordStatus(searchRun.WordLink.ParentWord, WordStatus.eInProgress);
                    OnExplProcessed(new ExplProcessedEventArgs(searchRun, false));
                    GetSemanticLink(searchRun);
                }
                else
                {
                    //searchRun.WordLink = lnk;
                }

                //m_SemanticHelper.UpdateWordStatus(curRun.WordLink.ParentWord, WordStatus.eNotProcessed);//WordStatus.eInProgress);
                //OnExplProcessed(new ExplProcessedEventArgs(curRun, false));

                //searchRun.WordLink = 
                //GetSemanticLink(searchRun);
                //WordSemanticLink link = GetSemanticLink(searchRun);
                //if (!m_ListLinks.Keys.Contains(searchRun.WordToSearch))
                //{
                //    m_ListLinks.Add(link.ParentWord.Word, link);
                //    OnExplProcessed(new ExplProcessedEventArgs(searchRun, true));
                //}

                int nNewDeepSearch = searchRun.DeepSearch - 1;
                foreach (WordLink aLink in searchRun.WordLink.Children)
                {
                    if (aLink.RelationType == WordLinkType.eNormal||
                        aLink.RelationType == WordLinkType.eTentative)
                    {
                        WordSemanticBranch childLink = m_SemanticHelper.GetSemanticLinkFromDB(aLink.Child.Word);
                        if (childLink == null)
                        {
                            m_SemanticHelper.UpdateWordStatus(aLink.Child, WordStatus.eNotProcessed);
                            childLink = new WordSemanticBranch();
                            childLink.ParentWord = aLink.Child;
                            m_SemanticHelper.AddDictionaryWord(childLink);                     
                        }
                        OnExplProcessed(new ExplProcessedEventArgs(new SearchRun(childLink, 0, false, 0), true));

                        ThreadPool.QueueUserWorkItem(new WaitCallback(this.StartSearch), new SearchRun(childLink, searchRun.DeepSearch - 1, searchRun.ForceSearch, searchRun.CurDeep + 1));
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}\n{1}", "CLientLogic.Search", ex.Message));
            }
        }

        WordSemanticBranch GetSemanticLink(SearchRun searchRun)
        {
            Options opt = new Options();
            opt.ForceSearch = searchRun.ForceSearch;
            opt.DeepSearch = searchRun.DeepSearch;

            searchRun.WordLink.Children = m_SemanticHelper.GetSemanticLink(searchRun.WordToSearch, opt).Children;//, searchRun.Granny);
            return searchRun.WordLink;
        }

        //private WordSemanticLink GetSemanticLink(SearchRun searchRun)
        //{
        //    return null;
        //    //WordSemanticLink link = semanticHelper.GetSemanticLink(searchRun.WordToSearch);
        //    //return link;
        //    //if (m_dbHelper.GetStatus(searchRun.WordToSearch) != WordStatus.eCompleted || searchRun.ForceSeacrh)
        //    //{
        //    //    WordSemanticLink link = semanticHelper.GetSemanticLink(searchRun.WordToSearch);
        //    //    return link;    
        //    //}
        //    //else
        //    //{
        //    //    WordSemanticLink link = GetSemanticLink(searchRun.WordToSearch);
        //    //    return link;
        //    //}

        //}

        private void UpdateDatabases(WordSemanticBranch aLink)
        {
            //WordItem anItem = new WordItem();
            //anItem.Word = aLink.
        }

        public void Search()
        {
            //try
            //{
            //    HyperChainServiceClient client = new HyperChainServiceClient();
            //    tbl_WordsTableAdapter adapter = new tbl_WordsTableAdapter();
            //    TableAdapterManager man = new TableAdapterManager();
            //    //man.tbl_WordsTableAdapter.
            //    //SqlCommand cm = new SqlCommand("Select * from tbl_Words Where Word = тест1");
            //    //adapter.Connection.Open();
            //    //string sh = adapter.Connection.ConnectionString;
            //    //string sConn = @"Data Source=d:\AS\_Dictionary\Project\!2013\03.25\HyperChain\HyperChain\Properties\DataSources\SemanticDB.sdf";// adapter.Connection.nConnectionString;
            //    //SqlCeDataAdapter adap = new SqlCeDataAdapter("Select * from tbl_Words Where Word = 'тест1'", adapter.Connection.ConnectionString);

            //    SqlCeCommand cm = adapter.Connection.CreateCommand();
            //    cm.CommandText = string.Format("Select * from tbl_Words Where Word = '{0}'", m_sWordToSearch);
            //    //cm.Connection = adapter.Connection;
            //    adapter.Adapter.SelectCommand = cm;//.CommandText = "Select * from tbl_Words Where Word = 'тест1'";
            //    adapter.Adapter.SelectCommand.CommandType = CommandType.Text;
            //    //adapter.Adapter.SelectCommand.Connection = adapter.Connection;

            //    SemanticDBDataSet.tbl_WordsDataTable m_tblWords = new SemanticDBDataSet.tbl_WordsDataTable();
            //    //adapter.Adapter.SelectCommand.E
            //    //cm.Connection.Open();
            //    ////int j = 
            //    //SqlCeResultSet  set = cm.ExecuteResultSet(ResultSetOptions.Scrollable);//();
            //    //cm.Connection.Close();
            //    adapter.Adapter.Fill(m_tblWords);
            //   // adap.Fill(m_tblWords);
            //    //adapter.Fill(m_tblWords);
            //    //adapter.FillBy(m_tblWords);
            //   // IEnumerable<DataRow> productsQuery =
            //   //         from tbl_Words in m_tblWords.AsEnumerable()
            //   //         select tbl_Words;

            //   // IEnumerable<DataRow> largeProducts =
            //   //     productsQuery.Where(p => p.Field<string>("Word") == m_sWordToSearch);

            //   //// Console.WriteLine("Products of size 'L':");
            //   // foreach (DataRow product in largeProducts)
            //   // {
            //   //     var s = product.Field<string>("Word");
            //   //     var s1 = product.Field<int>("Id");
            //   //     var s2 = product.Field<short>("Status");
            //   //     int w = 1; 
            //   // }
   
                

            //    //DataRow[] rows = m_tblWords.Select(word=>
            //    //    {
            //    //        W
            //    //    });//string.Format("Word={0}", m_sWordToSearch));
            //    //using (SqlConnection conn = new SqlConnection(SemanticDBDataSet.Dat))
            //    //{
            //    //      SqlCommand cmd = new SqlCommand("SELECT * FROM whatever 
            //    //                                       WHERE id = 5", conn);
            //    //        try
            //    //        {
            //    //            conn.Open();
            //    //            newID = (int)cmd.ExecuteScalar();
            //    //        }
            //    //        catch (Exception ex)
            //    //        {
            //    //            Console.WriteLine(ex.Message);
            //    //        }
            //    // }

            //    //m_tblWords.CreateDataReader().Read()
            //    //foreach (DataRow row in m_tblWords.Rows)
            //    //{

            //    //    string sWord = ((string)row.ItemArray[0]).ToUpper();
            //    //    string sWordChild = ((string)row.ItemArray[1]).ToUpper();
            //    //    //if (!m_WordsList.ContainsKey(sWord))
            //    //    //{
            //    //    //    TreeNode aNodeProp = new TreeNode();
            //    //    //    m_WordsList.Add(sWord, aNodeProp);
            //    //    //    aNodeProp.AddChild(sWordChild);
            //    //    //}
            //    //    //else
            //    //    //{
            //    //    //    m_WordsList[sWord].AddChild(sWordChild);
            //    //    //}

            //    //    //if (!m_WordsList.ContainsKey(sWordChild))
            //    //    //{
            //    //    //    TreeNode aNodeChild = new TreeNode();
            //    //    //    m_WordsList.Add(sWordChild, aNodeChild);
            //    //    //}
            //    //}
                
            //    WordSemanticLink link = client.GetSemanticLink(m_sWordToSearch);

            //    OnExplProcessed(new ExplProcessedEventArgs(0, 1));
            //    int q = 1;
            //}
            //catch (Exception ex)
            //{
            //    int h = 1;
            //}
        }

        public void StartSearch(string sWord)
        {
            m_sWordToSearch = sWord;
            Search();
            //Thread workerThread = new Thread(this.Search);
            
        }

        public void ShowHyperChain(string sWord)
        {

        }

        public void LoadWords()
        {

        }
    }
}
