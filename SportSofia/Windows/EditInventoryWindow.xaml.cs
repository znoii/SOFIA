using Microsoft.EntityFrameworkCore;
using SportSofia.Models;
using System.Linq;
using System.Windows;

namespace SportSofia.Windows
{
    public partial class EditInventoryWindow : Window
    {
        private readonly Inventory _inventory;

        public EditInventoryWindow(Inventory inventory)
        {
            InitializeComponent();
            _inventory = inventory;
            Loaded += EditInventoryWindow_Loaded;
        }

        private void EditInventoryWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InventoryNumberTextBox.Text = _inventory.InventoryNumber;
            NameTextBox.Text = _inventory.Name;
            TypeTextBox.Text = _inventory.Type;
            DescriptionTextBox.Text = _inventory.Description;

            StateComboBox.ItemsSource = System.Enum.GetValues(typeof(InventoryState)).Cast<InventoryState>();
            StateComboBox.SelectedItem = _inventory.State;

            UserComboBox.ItemsSource = App.DbContext.Users.ToList();
            UserComboBox.SelectedItem = _inventory.ReaderId.HasValue ?
                App.DbContext.Users.Find(_inventory.ReaderId.Value) : null;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _inventory.InventoryNumber = InventoryNumberTextBox.Text;
            _inventory.Name = NameTextBox.Text;
            _inventory.Type = TypeTextBox.Text;
            _inventory.Description = DescriptionTextBox.Text;
            _inventory.State = (InventoryState)StateComboBox.SelectedItem;
            _inventory.ReaderId = (UserComboBox.SelectedItem as User)?.Id;

            App.DbContext.SaveChanges();
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}