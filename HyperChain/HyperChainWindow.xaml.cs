using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SemanticLinkHelper;
using System.Collections.ObjectModel;
using System.Data;

namespace HyperChain
{
    /// <summary>
    /// Interaction logic for HyperChainWindow.xaml
    /// </summary>
    public partial class HyperChainWindow : Window
    {
        //ObservableCollection<List<string>> m_Hypers = new ObservableCollection<List<string>>();
        DataTable m_Hypers = new DataTable();

        public HyperChainWindow()
        {
            InitializeComponent();
            SearchLogic.HyperChainAdded += new ClientLogic.HyperChainAddedEventHandler(HyperChainAdded);
        }

        private ClientLogic SearchLogic
        {
            get { return ((App)App.Current).m_Logic; }
        }

        public delegate void UpdateUICallback(HyperChainAddedEventArgs e);

        void CreateTable(ShowHyperRun aRun)
        {
            //m_Hypers = new DataTable();
            for (int i = 0; i < aRun.DeepSearch; i++)
            {
                m_Hypers.Columns.Add((i + 1).ToString(), typeof(HyperChainItem));
            }

            GridView gv = new GridView();
            for (int i = 0; i < m_Hypers.Columns.Count; i++)
            {
                GridViewColumn col = new GridViewColumn();
                col.Header = (i + 1).ToString();
                col.DisplayMemberBinding = new Binding("[" + i + "].Word");
                col.Width = 150;
                gv.Columns.Add(col);
            }

            lvHyperChains.View = gv;
            lvHyperChains.ItemsSource = m_Hypers.Rows;
        }

        private void UpdateUI(HyperChainAddedEventArgs e)
        {
            if (m_Hypers == null)
                return;
            lock (m_Hypers)
            {
                if (m_Hypers.Columns.Count == 0)
                    CreateTable(e.CurrentRun);

                GridView grid = (GridView)lvHyperChains.View;
                if (m_Hypers.Rows.Count == 0)
                {
                    DataRow dr = m_Hypers.NewRow();
                    dr[e.CurrentRun.CurDeep - 1] = new HyperChainItem(e.CurrentRun.WordLink.ParentWord.Word);
                    m_Hypers.Rows.Add(dr);
                }
                else
                {
                    if (e.CurrentRun.NewBranch)
                    {
                        DataRow dr = m_Hypers.NewRow();
                        HyperChainItem newItem =  new HyperChainItem(e.CurrentRun.WordLink.ParentWord.Word);
                        //copy all prev row element
                        for (int i = 0; i < e.CurrentRun.CurDeep - 1; i++)
                        {
                            HyperChainItem curItem = m_Hypers.Rows[m_Hypers.Rows.Count - 1][i] as HyperChainItem;
                            dr[i] = curItem;
                            if (curItem == null || newItem.Word.Equals(curItem.Word, StringComparison.OrdinalIgnoreCase))
                            {
                                newItem.Found = true;
                            }
                        }

                        if (m_Hypers.Columns.Count > e.CurrentRun.CurDeep - 1)
                        {
                            HyperChainItem prevItem = m_Hypers.Rows[m_Hypers.Rows.Count - 1][e.CurrentRun.CurDeep - 2] as HyperChainItem;
                            if (prevItem == null || prevItem.Found || prevItem.Word.Length == 0)
                            {
                                dr[e.CurrentRun.CurDeep - 1] = null;
                                //to avoid duplicates
                                //m_Hypers.Rows.Add(dr);
                            }
                            else
                            {
                                dr[e.CurrentRun.CurDeep - 1] = newItem;
                                m_Hypers.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            dr[e.CurrentRun.CurDeep - 1] = newItem;
                            m_Hypers.Rows.Add(dr);
                        }                        
                        
                    }
                    else
                    {
                        HyperChainItem newItem = new HyperChainItem(e.CurrentRun.WordLink.ParentWord.Word);
                        //find same element
                        for (int i = 0; i < e.CurrentRun.CurDeep - 1; i++)
                        {
                            HyperChainItem curItem = m_Hypers.Rows[m_Hypers.Rows.Count - 1][i] as HyperChainItem;
                            if (curItem == null || curItem.Found || curItem.Word.Length == 0 ||
                                newItem.Word.Equals(curItem.Word, StringComparison.OrdinalIgnoreCase))
                            {
                                newItem.Found = true;
                                break;
                            }
                        }

                        if (m_Hypers.Columns.Count > e.CurrentRun.CurDeep - 1)
                        {
                            HyperChainItem prevItem = m_Hypers.Rows[m_Hypers.Rows.Count - 1][e.CurrentRun.CurDeep - 2] as HyperChainItem;
                            if (prevItem == null || prevItem.Found || prevItem.Word.Length == 0)
                                m_Hypers.Rows[m_Hypers.Rows.Count - 1][e.CurrentRun.CurDeep - 1] = null;
                            else
                                m_Hypers.Rows[m_Hypers.Rows.Count - 1][e.CurrentRun.CurDeep - 1] = newItem;
                        }
                        else
                        {
                            m_Hypers.Rows[m_Hypers.Rows.Count - 1][e.CurrentRun.CurDeep - 1] = newItem;
                        }
                    }
                }         
                
                

                //foreach (WordLink lnk in e.CurrentRun.WordLink.Children)
                //{
                //    List<string> vStrings = new List<string>();
                //    m_Hypers.Add(vStrings);
                //    vStrings.Add(e.CurrentRun.WordLink.ParentWord.Word);

                //    lvHyperChains.Items.Add(lnk.Child);
                //}
                lvHyperChains.Items.Refresh();
                if (lvHyperChains.Items.Count > 0)
                {

                    //Style st = new Style();
                    //st.TargetType = typeof(ListViewItem);
                    
                    //Setter backGroundSetter = new Setter();
                    //backGroundSetter.Property = ListViewItem.BackgroundProperty;
                    //ListView listView =
                    //    ItemsControl.ItemsControlFromItemContainer(container)
                    //      as ListView;
                    //int index =
                      //  lvHyperChains.ItemContainerGenerator.IndexFromContainer(container);
                    //if (index % 2 == 0)
                    //{
                    //    backGroundSetter.Value = Brushes.LightBlue;
                    //}
                    //else
                    

                    //ListViewItem item = lvHyperChains.ItemContainerGenerator.ContainerFromItem(lvHyperChains.Items[0]) as ListViewItem;

                    //DataRow dr = (DataRow)GetValue();
                    //if (dr.ItemArray[0] == "тест1")
                    //{
                    //    backGroundSetter.Value = Brushes.Red;
                    //}
                    //st.Setters.Add(backGroundSetter);
                    //item.Style = st;
                    //item.Background = Brushes.Red;
                }
            }
        }

        private void HyperChainAdded(object sender, HyperChainAddedEventArgs e)
        {
            this.Dispatcher.Invoke(new UpdateUICallback(this.UpdateUI),
                                    new object[] { e });
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            m_Hypers.Clear();
            m_Hypers = null;
            Close();
        }
    }

    class HyperChainItem
    {
        public string Word { get; set; }
        public bool Found { get; set; }
        
        public HyperChainItem(string sWord)
        {
            Word = sWord;
            Found = false;
        }
    }
}
