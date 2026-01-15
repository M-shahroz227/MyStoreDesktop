using System.Data.Entity;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Data
{
    public class DatabaseHelper : DbContext
    {
        public DatabaseHelper() : base("name=DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillProduct> BillProducts { get; set; }
        public DbSet<QrTableData> QrTableDatas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<CustomerInvoice> CustomerInvoices { get; set; }
        public DbSet<Setting> Settings { get; set; }

        // 🔁 RETURNS
        public DbSet<Return> Returns { get; set; }
        public DbSet<ReturnItem> ReturnItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // ================= BILL =================

            // Bill → BillProducts (CASCADE)
            modelBuilder.Entity<Bill>()
                .HasMany(b => b.BillProducts)
                .WithRequired(bp => bp.Bill)
                .HasForeignKey(bp => bp.BillId)
                .WillCascadeOnDelete(true);

            // Bill → Returns (NO CASCADE ❌)
            modelBuilder.Entity<Bill>()
                .HasMany(b => b.Returns)
                .WithRequired(r => r.Bill)
                .HasForeignKey(r => r.BillId)
                .WillCascadeOnDelete(false);

            // ================= PRODUCTS =================

            // Product → BillProducts (NO CASCADE)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.BillProducts)
                .WithRequired(bp => bp.Product)
                .HasForeignKey(bp => bp.ProductId)
                .WillCascadeOnDelete(false);

            // ================= RETURNS =================

            // Return → ReturnItems (CASCADE ✅)
            modelBuilder.Entity<Return>()
                .HasMany(r => r.ReturnItems)
                .WithRequired(ri => ri.Return)
                .HasForeignKey(ri => ri.ReturnId)
                .WillCascadeOnDelete(true);

            // ================= USERS =================

            // User → Bills (NO CASCADE)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Bills)
                .WithRequired(b => b.User)
                .HasForeignKey(b => b.UserId)
                .WillCascadeOnDelete(false);

            // ================= CATEGORIES =================

            // Category → Products (NO CASCADE)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithRequired(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .WillCascadeOnDelete(false);

            // ================= COMPANIES =================

            // Company → Products (NO CASCADE)
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Products)
                .WithRequired(p => p.Company)
                .HasForeignKey(p => p.CompanyId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
