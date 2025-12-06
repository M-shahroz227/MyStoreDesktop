namespace MyStoreDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QrTableDatas", "CodeValue", c => c.String(nullable: false));
            AddColumn("dbo.QrTableDatas", "CodeType", c => c.String(nullable: false));
            DropColumn("dbo.QrTableDatas", "QrCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QrTableDatas", "QrCode", c => c.Guid(nullable: false));
            DropColumn("dbo.QrTableDatas", "CodeType");
            DropColumn("dbo.QrTableDatas", "CodeValue");
        }
    }
}
