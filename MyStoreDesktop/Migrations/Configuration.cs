using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MyStoreDesktop.Data.DatabaseHelper>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyStoreDesktop.Data.DatabaseHelper context)
        {
            // 1️⃣ Seed Companies first (for FK in Products)
            context.Companies.AddOrUpdate(
                c => c.Title,
                new Company { Title = "Dell", Description = "Dell Electronics" },
                new Company { Title = "HP", Description = "HP Computers" },
                new Company { Title = "Logitech", Description = "Computer Accessories" }
            );
            context.SaveChanges();

            // Get inserted company records
            var dell = context.Companies.FirstOrDefault(c => c.Title == "Dell");
            var hp = context.Companies.FirstOrDefault(c => c.Title == "HP");
            var logitech = context.Companies.FirstOrDefault(c => c.Title == "Logitech");

            // 2️⃣ Seed Categories
            context.Categories.AddOrUpdate(
                c => c.Title,
                new Category { Title = "Electronics" },
                new Category { Title = "Accessories" },
                new Category { Title = "Stationery" }
            );
            context.SaveChanges();

            // Get inserted category records
            var electronics = context.Categories.FirstOrDefault(c => c.Title == "Electronics");
            var accessories = context.Categories.FirstOrDefault(c => c.Title == "Accessories");

            // 3️⃣ Seed Products (link both Company + Category)
            context.Products.AddOrUpdate(
                p => p.Title,
                new Product
                {
                    Title = "Keyboard",
                    SalePrice = 1500,
                    PurchasePrice = 1000,
                    Quantity = 100,
                    Discount = 0,
                    CategoryId = accessories.CategoryId,
                    CompanyId = logitech.CompanyId
                },
                new Product
                {
                    Title = "Mouse",
                    SalePrice = 800,
                    PurchasePrice = 500,
                    Quantity = 200,
                    Discount = 0,
                    CategoryId = accessories.CategoryId,
                    CompanyId = logitech.CompanyId
                },
                new Product
                {
                    Title = "Monitor",
                    SalePrice = 12000,
                    PurchasePrice = 8000,
                    Quantity = 50,
                    Discount = 0,
                    CategoryId = electronics.CategoryId,
                    CompanyId = dell.CompanyId
                }
            );
            context.SaveChanges();

            // 4️⃣ Seed Admin User
            string adminPassword = "PakBahLah@2";
            byte[] passwordHash = Encoding.UTF8.GetBytes(adminPassword);
            byte[] passwordSalt = new byte[0]; // For compatibility

            context.Users.AddOrUpdate(
                u => u.UserName,
                new User
                {
                    UserName = "admin",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Email = "admin@mystore.com",
                    Phone = ""
                }
            );

            context.SaveChanges();
        }
    }
}
