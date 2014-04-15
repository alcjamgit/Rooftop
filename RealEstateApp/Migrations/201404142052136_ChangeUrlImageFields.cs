namespace RealEstateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUrlImageFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RealtyAdImages", "FileName", c => c.String());
            DropColumn("dbo.RealtyAds", "DefaultImageUrl");
            DropColumn("dbo.RealtyAdImages", "Url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RealtyAdImages", "Url", c => c.String());
            AddColumn("dbo.RealtyAds", "DefaultImageUrl", c => c.String());
            DropColumn("dbo.RealtyAdImages", "FileName");
        }
    }
}
