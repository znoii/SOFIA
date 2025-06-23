using Microsoft.EntityFrameworkCore;
using SportSofia.Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SportSofia
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static AppDbContext? _dbContext;
        public static AppDbContext DbContext => _dbContext ??= new AppDbContext();
        public static User? CurrentUser { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DbContext.Users.Load();
            DbContext.Inventories.Load();
            DbContext.Roles.Load();
        }
    }

}
