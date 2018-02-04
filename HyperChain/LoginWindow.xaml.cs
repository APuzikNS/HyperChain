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

namespace HyperChain
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            edUserName.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (((App)App.Current).m_Logic.Login(edUserName.Text.Trim(), edPassword.Password.Trim()))
            {
                App.Current.MainWindow = new MainWindow();
                App.Current.MainWindow.Show();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
