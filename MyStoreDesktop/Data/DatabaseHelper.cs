using System.Data.Entity;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Data
{
    public class DatabaseHelper : DbContext   // 👈 Name changed
    {
        public DatabaseHelper() : base("name=DefaultConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillProduct> BillProducts { get; set; }
        public DbSet<QrTableData> QrTableDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Bill → BillProducts (1-Many)
            modelBuilder.Entity<Bill>()
                .HasMany(b => b.BillProducts)
                .WithRequired(bp => bp.Bill)
                .HasForeignKey(bp => bp.BillId)
                .WillCascadeOnDelete(true);

            // Product → BillProducts (1-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.BillProducts)
                .WithRequired(bp => bp.Product)
                .HasForeignKey(bp => bp.ProductId)
                .WillCascadeOnDelete(false);

            // User → Bills (1-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Bills)
                .WithRequired(b => b.User)
                .HasForeignKey(b => b.UserId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
