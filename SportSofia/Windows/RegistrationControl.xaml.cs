using SportSofia.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SportSofia.Windows
{
    public partial class RegistrationControl : UserControl
    {
        public RegistrationControl()
        {
            InitializeComponent();
        }

        private void OnBackToLoginClick(object sender, RoutedEventArgs e)
        {
            if (this.Parent is Window win)
                win.Content = new LoginControl();
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                MessageBox.Show("Passwords don't match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newUser = new User
            {
                Login = LoginTextBox.Text,
                Password = PasswordBox.Password,
                Surname = SurnameTextBox.Text,
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                RegistrationDate = DateOnly.FromDateTime(DateTime.Now),
                RoleId = 2 // Default role is User
            };

            try
            {
                App.DbContext.Users.Add(newUser);
                App.DbContext.SaveChanges();

                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                if (this.Parent is Window win)
                    win.Content = new LoginControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}