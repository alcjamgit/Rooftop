namespace RealEstateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBedCountToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RealtyAds", "BedCount", c => c.Short());
            AlterColumn("dbo.RealtyAds", "BathCount", c => c.Short());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RealtyAds", "BathCount", c => c.Short(nullable: false));
            AlterColumn("dbo.RealtyAds", "BedCount", c => c.Short(nullable: false));
        }
    }
}
