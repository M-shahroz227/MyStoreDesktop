namespace MyStoreDesktop.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyStoreDesktop.Data.DatabaseHelper>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyStoreDesktop.Data.DatabaseHelper context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            // Seed default admin user
            string adminPassword = "PakBahLah@2";
            byte[] passwordHash = System.Text.Encoding.UTF8.GetBytes(adminPassword);
            byte[] passwordSalt = new byte[0]; // Using the same approach as RegisterForm

            context.Users.AddOrUpdate(
                u => u.UserName, // Key selector to check for existing user
                new Models.User
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
