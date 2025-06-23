using Microsoft.EntityFrameworkCore;
using SportSofia.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SportSofia.Windows
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            LoadInventory();
        }

        private void LoadInventory(bool showOnlyMine = false)
        {
            var query = App.DbContext.Inventories
                .Include(i => i.Reader)
                .AsQueryable();

            if (showOnlyMine)
            {
                query = query.Where(i => i.ReaderId == App.CurrentUser.Id);
            }

            var inventoryList = query.ToList().Select(i => new InventoryViewModel
            {
                Id = i.Id,
                InventoryNumber = i.InventoryNumber,
                Name = i.Name,
                Type = i.Type,
                State = i.State,
                CanTake = i.State == InventoryState.В_наличии && !showOnlyMine,
                CanReturn = i.State == InventoryState.Выдана && i.ReaderId == App.CurrentUser.Id
            }).ToList();

            InventoryListView.ItemsSource = inventoryList;
        }

        private void ShowAllInventory_Click(object sender, RoutedEventArgs e)
        {
            LoadInventory(false);
        }

        private void ShowMyInventory_Click(object sender, RoutedEventArgs e)
        {
            LoadInventory(true);
        }

        private void TakeInventory_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is InventoryViewModel item)
            {
                var inventory = App.DbContext.Inventories.Find(item.Id);
                if (inventory != null)
                {
                    inventory.State = InventoryState.Выдана;
                    inventory.ReaderId = App.CurrentUser.Id;
                    App.DbContext.SaveChanges();
                    LoadInventory();
                }
            }
        }

        private void ReturnInventory_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is InventoryViewModel item)
            {
                var inventory = App.DbContext.Inventories.Find(item.Id);
                if (inventory != null)
                {
                    inventory.State = InventoryState.В_наличии;
                    inventory.ReaderId = null;
                    App.DbContext.SaveChanges();
                    LoadInventory(true);
                }
            }
        }
    }

    public class InventoryViewModel
    {
        public int Id { get; set; }
        public string InventoryNumber { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public InventoryState State { get; set; }
        public bool CanTake { get; set; }
        public bool CanReturn { get; set; }
    }
}