namespace RealEstateApp.MigrationsAppDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RealtyAds", "DatePosted", c => c.DateTime(nullable: false));
            AddColumn("dbo.RealtyAds", "UserId", c => c.String());
            AddColumn("dbo.RealtyAds", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.RealtyAds", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RealtyAds", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.RealtyAds", "Address", c => c.String());
            AddColumn("dbo.RealtyAds", "City", c => c.Long(nullable: false));
            AddColumn("dbo.RealtyAds", "BedCount", c => c.Int(nullable: false));
            AddColumn("dbo.RealtyAds", "BathCount", c => c.Int(nullable: false));
            AddColumn("dbo.RealtyAds", "FloorAreaSqM", c => c.Single(nullable: false));
            AddColumn("dbo.RealtyAds", "LotAreaSqM", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RealtyAds", "LotAreaSqM");
            DropColumn("dbo.RealtyAds", "FloorAreaSqM");
            DropColumn("dbo.RealtyAds", "BathCount");
            DropColumn("dbo.RealtyAds", "BedCount");
            DropColumn("dbo.RealtyAds", "City");
            DropColumn("dbo.RealtyAds", "Address");
            DropColumn("dbo.RealtyAds", "Type");
            DropColumn("dbo.RealtyAds", "Price");
            DropColumn("dbo.RealtyAds", "Status");
            DropColumn("dbo.RealtyAds", "UserId");
            DropColumn("dbo.RealtyAds", "DatePosted");
        }
    }
}
