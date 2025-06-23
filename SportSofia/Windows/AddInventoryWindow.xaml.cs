using SportSofia.Models;
using System.Windows;

namespace SportSofia.Windows
{
    public partial class AddInventoryWindow : Window
    {
        public AddInventoryWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newInventory = new Inventory
            {
                InventoryNumber = InventoryNumberTextBox.Text,
                Name = NameTextBox.Text,
                Type = TypeTextBox.Text,
                Description = DescriptionTextBox.Text,
                PublicationDate = DateOnly.FromDateTime(DateTime.Now),
                State = InventoryState.В_наличии
            };

            try
            {
                App.DbContext.Inventories.Add(newInventory);
                App.DbContext.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}