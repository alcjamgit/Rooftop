namespace RealEstateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rev01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RealtyAds", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.RealtyAds", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.RealtyAds", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.RealtyAds", "ApplicationUser_Id");
            AddForeignKey("dbo.RealtyAds", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RealtyAds", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.RealtyAds", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.RealtyAds", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.RealtyAds", "ApplicationUser_Id");
            AddForeignKey("dbo.RealtyAds", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
