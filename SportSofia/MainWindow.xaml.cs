using SportSofia.Models;
using SportSofia.Windows;
using System.Windows;
using System.Windows.Controls;

namespace SportSofia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            if (loginWindow.ShowDialog() == true)
            {
                if (App.CurrentUser.RoleId == 1) // Admin
                {
                    MainFrame.Navigate(new AdminControl());
                }
                else // User
                {
                    MainFrame.Navigate(new UserControl1());
                }
            }
            else
            {
                Close();
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            MainWindow_Loaded(sender, e);
        }
    }
}