namespace MyStoreDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Total : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Total");
        }
    }
}
