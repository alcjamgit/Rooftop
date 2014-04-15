namespace RealEstateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableRealtyAdImageDefaults : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RealtyAdImageDefaults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RealtyAd_Id = c.Int(nullable: false),
                        RealtyAdImage_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RealtyAds", t => t.RealtyAd_Id, cascadeDelete: false)
                .ForeignKey("dbo.RealtyAdImages", t => t.RealtyAdImage_Id, cascadeDelete: false)
                .Index(t => t.RealtyAd_Id)
                .Index(t => t.RealtyAdImage_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RealtyAdImageDefaults", "RealtyAdImage_Id", "dbo.RealtyAdImages");
            DropForeignKey("dbo.RealtyAdImageDefaults", "RealtyAd_Id", "dbo.RealtyAds");
            DropIndex("dbo.RealtyAdImageDefaults", new[] { "RealtyAdImage_Id" });
            DropIndex("dbo.RealtyAdImageDefaults", new[] { "RealtyAd_Id" });
            DropTable("dbo.RealtyAdImageDefaults");
        }
    }
}
