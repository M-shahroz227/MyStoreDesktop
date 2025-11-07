namespace MyStoreDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ChangeQrCodeToGuid : DbMigration
    {
        public override void Up()
        {
            // Drop existing data and convert column to GUID
            Sql("DELETE FROM dbo.QrTableDatas"); // Clear existing data
            Sql("ALTER TABLE dbo.QrTableDatas DROP COLUMN QrCode");
            Sql("ALTER TABLE dbo.QrTableDatas ADD QrCode UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID()");
        }

        public override void Down()
        {
            // Revert back to string
            Sql("ALTER TABLE dbo.QrTableDatas DROP COLUMN QrCode");
            Sql("ALTER TABLE dbo.QrTableDatas ADD QrCode NVARCHAR(255) NULL");
        }
    }
}
