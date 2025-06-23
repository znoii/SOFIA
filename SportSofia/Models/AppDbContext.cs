using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportSofia.Models
{
    public class AppDbContext : DbContext
    {
        private static readonly Role[] roles = [
        new Role {
            Id = 1,
            Name = "Администратор",
            AccessRights = "Полные права"
        },
        new Role {
            Id = 2,
            Name = "Пользователь",
            AccessRights = "Базовые права"
        }
    ];

        // Пользователи
        private static readonly User[] users = [
            new User {
            Id = 1,
            Login = "admin",
            Password = "123",
            RegistrationDate = DateOnly.FromDateTime(DateTime.Now),
            Surname = "Иванов",
            Name = "Иван",
            Phone = "+79991234567",
            RoleId = 1
        },
        new User {
            Id = 2,
            Login = "user",
            Password = "456",
            RegistrationDate = DateOnly.FromDateTime(DateTime.Now),
            Surname = "Петров",
            Name = "Петр",
            Phone = "+79997654321",
            RoleId = 2
        }
        ];

        // Инвентарь
        private static readonly Inventory[] inventories = [
            new Inventory {
            Id = 1,
            InventoryNumber = "INV-001",
            Name = "Ноутбук",
            Type = "Техника",
            Description = "Ноутбук Dell XPS 15",
            PublicationDate = new DateOnly(2023, 1, 15),
            State = InventoryState.В_наличии
        },
        new Inventory {
            Id = 2,
            InventoryNumber = "INV-002",
            Name = "Проектор",
            Type = "Техника",
            Description = "Проектор Epson EB-U05",
            PublicationDate = new DateOnly(2023, 2, 20),
            State = InventoryState.Выдана,
            ReaderId = 2
        }
        ];
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Inventory>().HasData(inventories);
        }
    }
}