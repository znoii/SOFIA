using SportSofia.Models;
using System.Windows;
using System.Windows.Controls;

namespace SportSofia.Windows
{
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();
        }

        private void OnRegistrationClick(object sender, RoutedEventArgs e)
        {
            if (this.Parent is Window win)
                win.Content = new RegistrationControl();
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            var user = App.DbContext.Users.FirstOrDefault(
                u => u.Login == LoginTextBox.Text && u.Password == PasswordBox.Password);

            if (user != null)
            {
                App.CurrentUser = user;
                if (this.Parent is Window win)
                {
                    win.DialogResult = true;
                    win.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid login or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}