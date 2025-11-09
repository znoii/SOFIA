using Microsoft.EntityFrameworkCore;
using ShoesCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShoesCompany.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginBox.Text;
            var password = PasswordBox.Password;

            if (login == null || password == null)
            {
                MessageBox.Show("Введите логин и пароль.");
                return;
            }

            using var context = new ShoesCompanyContext();
            var user = context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                var productsWindow = new ProductsWindow(user); 
                productsWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
            }
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            var productsWindow = new ProductsWindow(null); 
            productsWindow.Show();
            this.Close();
        }
    }
}
