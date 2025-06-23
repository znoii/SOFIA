using Microsoft.EntityFrameworkCore;
using SportSofia.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SportSofia.Windows
{
    public partial class AdminControl : System.Windows.Controls.UserControl
    {
        public AdminControl()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            InventoryListView.ItemsSource = App.DbContext.Inventories
                .Include(i => i.Reader)
                .ToList();

            UsersListView.ItemsSource = App.DbContext.Users
                .Include(u => u.Role)
                .ToList();
        }

        private void AddInventory_Click(object sender, RoutedEventArgs e)
        {
            AddInventoryWindow window = new AddInventoryWindow();
            if (window.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void EditInventory_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryListView.SelectedItem is Inventory selected)
            {
                EditInventoryWindow window = new EditInventoryWindow(selected);
                if (window.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select an inventory item", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteInventory_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryListView.SelectedItem is Inventory selected)
            {
                if (MessageBox.Show("Are you sure you want to delete this item?", "Confirm",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.DbContext.Inventories.Remove(selected);
                    App.DbContext.SaveChanges();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select an inventory item", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}