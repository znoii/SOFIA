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
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        private readonly User? _currentUser;

        public ProductsWindow(User? user)
        {
            InitializeComponent();
            _currentUser = user;

            if (_currentUser != null)
            {
                UserFullNameText.Text = _currentUser.FullName;
            }
            else UserFullNameText.Text = "";

            LoadProducts();
        }

        private void LoadProducts()
        {
            using var context = new ShoesCompanyContext();
            var products = context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Provider)
                .ToList();

            var displayItems = products.Select(p =>
            {
                decimal price = p.Price;
                int discount = p.CurrentDiscount;
                decimal newPrice = price - (price * discount / 100);

                Brush bg = Brushes.White;
                if (p.Count == 0)
                    bg = Brushes.LightBlue;
                else if (discount > 15)
                    bg = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57"));

                string originalPriceText = $"{price:F2} руб.";
                string discountedPriceText = discount > 0 ? $"{newPrice:F2} руб." : null;

                return new
                {
                    p.Article,
                    CategoryAndName = $"{p.Category.Name} | {p.Name}",
                    p.Description,
                    Manufacturer = p.Manufacturer.Name,
                    Provider = p.Provider.Name,
                    p.Unit,
                    p.Count,
                    Price = p.Price,
                    IsDiscounted = discount > 0,
                    IsBigDiscount = discount > 15,
                    DiscountText = discount > 0 ? $"Скидка {discount}%" : "—",
                    UnitString = $"Единица измерения: {p.Unit}",
                    StockString = $"Количество на складе: {p.Count}",
                    PhotoPath = string.IsNullOrEmpty(p.PhotoPath)
                        ? "C:\\C#\\ShoesCompany\\images\\picture.png"
                        : $"C:\\C#\\ShoesCompany\\images\\{p.PhotoPath}",
                    CurrentDiscount = discount,
                    BackgroundBrush = bg,
                    HasDiscount = discount > 0,
                    OriginalPriceText = originalPriceText,        
                    DiscountedPriceText = discountedPriceText,
                };
            }).ToList();

            ProductsList.ItemsSource = displayItems;
        }

    }
}
