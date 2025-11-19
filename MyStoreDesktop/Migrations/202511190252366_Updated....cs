namespace MyStoreDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillProducts", "Title", c => c.String());
            AddColumn("dbo.BillProducts", "SalePrice", c => c.Double(nullable: false));
            AddColumn("dbo.BillProducts", "Total", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BillProducts", "Total");
            DropColumn("dbo.BillProducts", "SalePrice");
            DropColumn("dbo.BillProducts", "Title");
        }
    }
}
