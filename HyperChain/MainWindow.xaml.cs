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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using System.Threading;
using System.Collections.ObjectModel;
using SemanticLinkHelper;

namespace HyperChain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {
            InitializeComponent();
            ((App)App.Current).m_Logic.ExplProcessed += new ClientLogic.ExplProcessedEventHandler(ExplProcessed);
            
            lvRegisrtyWords.ItemsSource = SearchLogic.ListWords;
            lvRuns.ItemsSource = SearchLogic.ListRuns;
        }

        ObservableCollection<WordSemanticBranch> ListWords
        {
            get { return SearchLogic.ListWords; }
            set { SearchLogic.ListWords = value; }
        }

        ObservableCollection<SearchRun> ListRuns
        {
            get { return SearchLogic.ListRuns; }
            set { SearchLogic.ListRuns = value; }
        }

        private ClientLogic SearchLogic
        {
            get { return ((App)App.Current).m_Logic; }
        }

        public delegate void UpdateTextCallback(ExplProcessedEventArgs e);

        private void UpdateUI(ExplProcessedEventArgs e)
        {
            int nIndex = ListRuns.IndexOf(e.CurrentRun);
            if (nIndex < 0)
                ListRuns.Add(e.CurrentRun);
            else
                ListRuns[nIndex] = e.CurrentRun;

            lvRegisrtyWords.Items.Refresh();
            lvRuns.Items.Refresh();
        }

        private void ExplProcessed(object sender, ExplProcessedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new UpdateTextCallback(this.UpdateUI),
                                    new object[] { e });
        }
        
        private void btnSearchWord_Click(object sender, RoutedEventArgs e)
        {
            string sWord = cmbWord.Text.Trim();
            SearchLogic.GoToWord(sWord);
        }

        private void btnSearchHyper_Click(object sender, RoutedEventArgs e)
        {
            string sWord = ((WordSemanticBranch)lvRegisrtyWords.SelectedItem).ParentWord.Word;
            ThreadPool.QueueUserWorkItem(new WaitCallback(SearchLogic.StartSearch), new SearchRun(((WordSemanticBranch)lvRegisrtyWords.SelectedItem), (int)EdDeepSearch.Value, (bool)ForceSearch.IsChecked, 1));
        }

        private void btnShowHyper_Click(object sender, RoutedEventArgs e)
        {
            Interlocked.Increment(ref SearchLogic.StopCreateHyper);
            ThreadPool.QueueUserWorkItem(new WaitCallback(SearchLogic.StartCreateHyper), new ShowHyperRun(((WordSemanticBranch)lvRegisrtyWords.SelectedItem), (int)edDeepHyper.Value, 1, true));
            HyperChainWindow wnd = new HyperChainWindow();
            wnd.ShowDialog();
            Interlocked.Decrement(ref SearchLogic.StopCreateHyper);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void lvRegisrtyWords_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            double d = e.ViewportWidthChange;
            double d1 = e.VerticalOffset;
            if (e.VerticalOffset + e.ViewportHeight >= ListWords.Count)
            {
                SearchLogic.LoadPage(lvRegisrtyWords.Items.Count); 
            }         
        }

        private void ShowContext(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                ContextMenu menu = new ContextMenu();
                MenuItem mi = new MenuItem();
                mi.Header = "Редагувати...";
                menu.Items.Add(mi);
                mi.Click += new RoutedEventHandler(Edit_Handler);
                menu.IsOpen = true;

            }
            else if (e.ChangedButton == MouseButton.Left)
            {
                //Edit_Handler(this, e);
            }
        }

        private void Edit_Handler(object sender, EventArgs e)
        {
            WordSemanticBranch aLink = (WordSemanticBranch)lvRegisrtyWords.SelectedItem;
           
            //SemanticHelper h = new SemanticHelper();
            //aLink = h.GetSemanticLinkFromDB("тест2");

            if (aLink == null)
                return;                      

            EditRelationsWindow wnd = new EditRelationsWindow(aLink);
            bool? bRes = wnd.ShowDialog();
            if (bRes != null && bRes == true )
            {
                CollectionViewSource.GetDefaultView(SearchLogic.ListWords).Refresh();
            }
        }

        private void ShowContextRuns(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                ContextMenu menu = new ContextMenu();
                MenuItem mi = new MenuItem();
                mi.Header = "Редагувати...";
                menu.Items.Add(mi);
                mi.Click += new RoutedEventHandler(Edit_HandlerRuns);
                menu.IsOpen = true;
            }
            else if (e.ChangedButton == MouseButton.Left)
            {
                //Edit_HandlerRuns(this, e);
            }
        }

        private void Edit_HandlerRuns(object sender, EventArgs e)
        {
            WordSemanticBranch aLink = null;
            
            SearchRun selectedRun = (SearchRun)lvRuns.SelectedItem;
            if( selectedRun != null )
                aLink = selectedRun.WordLink;

            if (aLink == null)
                return;

            EditRelationsWindow wnd = new EditRelationsWindow(aLink);
            bool? bRes = wnd.ShowDialog();
            if (bRes != null && bRes == true)
            {
                CollectionViewSource.GetDefaultView(SearchLogic.ListWords).Refresh();
            }
        }


    }
}
