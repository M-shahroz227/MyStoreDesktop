namespace MyStoreDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillProducts",
                c => new
                    {
                        BillProductId = c.Int(nullable: false, identity: true),
                        BillId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Title = c.String(),
                        SalePrice = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.BillProductId)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.BillId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CustomerInvoiceId = c.Int(nullable: false),
                        Role = c.String(),
                        BillDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        OwnDate = c.DateTime(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        itemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrandTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentMethod = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.CustomerInvoices", t => t.CustomerInvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CustomerInvoiceId);
            
            CreateTable(
                "dbo.CustomerInvoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        CustomerPhone = c.String(),
                        CustomerAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.Binary(nullable: false),
                        PasswordSalt = c.Binary(nullable: false),
                        Email = c.String(maxLength: 150),
                        Phone = c.String(maxLength: 20),
                        Role = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(maxLength: 255),
                        CodeType = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        CompanyId = c.Int(nullable: false),
                        Model = c.String(),
                        Quantity = c.Int(nullable: false),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryId = c.Int(nullable: false),
                        UrlImage = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => t.CompanyId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Configurations",
                c => new
                    {
                        ConfigId = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ConfigId);
            
            CreateTable(
                "dbo.QrTableDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        CodeValue = c.String(nullable: false),
                        CodeType = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QrTableDatas", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.BillProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Bills", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bills", "CustomerInvoiceId", "dbo.CustomerInvoices");
            DropForeignKey("dbo.BillProducts", "BillId", "dbo.Bills");
            DropIndex("dbo.QrTableDatas", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Products", new[] { "CompanyId" });
            DropIndex("dbo.Bills", new[] { "CustomerInvoiceId" });
            DropIndex("dbo.Bills", new[] { "UserId" });
            DropIndex("dbo.BillProducts", new[] { "ProductId" });
            DropIndex("dbo.BillProducts", new[] { "BillId" });
            DropTable("dbo.Settings");
            DropTable("dbo.QrTableDatas");
            DropTable("dbo.Configurations");
            DropTable("dbo.Companies");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Users");
            DropTable("dbo.CustomerInvoices");
            DropTable("dbo.Bills");
            DropTable("dbo.BillProducts");
        }
    }
}
