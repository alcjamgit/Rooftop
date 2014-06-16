namespace RealEstateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Region = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RealtyAdImageDefaults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RealtyAd_Id = c.Int(nullable: false),
                        RealtyAdImage_Id = c.Long(nullable: false),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RealtyAds", t => t.RealtyAd_Id, cascadeDelete: true)
                .ForeignKey("dbo.RealtyAdImages", t => t.RealtyAdImage_Id, cascadeDelete: false)
                .Index(t => t.RealtyAd_Id)
                .Index(t => t.RealtyAdImage_Id);
            
            CreateTable(
                "dbo.RealtyAds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortDescn = c.String(nullable: false, maxLength: 50),
                        LongDescn = c.String(),
                        DatePosted = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Category = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Address = c.String(maxLength: 255),
                        BedCount = c.Short(),
                        BathCount = c.Short(),
                        FloorAreaSqM = c.Single(),
                        LotAreaSqM = c.Single(),
                        City_Id = c.Int(nullable: false),
                        Latitude = c.Single(nullable: false),
                        Longitude = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.City_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.City_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Email = c.String(maxLength: 50),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        CellphoneNum = c.String(maxLength: 30),
                        TelephoneNum = c.String(maxLength: 30),
                        DateJoined = c.DateTime(),
                        ProfilePhotoFileName = c.String(maxLength: 150),
                        IsRealtyAgent = c.Boolean(),
                        AboutMessage = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RealtyAdImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Caption = c.String(),
                        RealtyAd_Id = c.Int(nullable: false),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RealtyAds", t => t.RealtyAd_Id, cascadeDelete: true)
                .Index(t => t.RealtyAd_Id);
            
            CreateTable(
                "dbo.RealtyAdMessages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RealtyAd_Id = c.Int(nullable: false),
                        DatePosted = c.DateTime(nullable: false),
                        PosterId = c.String(),
                        Email = c.String(),
                        CellphoneNum = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RealtyAds", t => t.RealtyAd_Id, cascadeDelete: true)
                .Index(t => t.RealtyAd_Id);
            
            CreateTable(
                "dbo.RealtyAdPageViews",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RealtyAd_Id = c.Int(nullable: false),
                        DateViewed = c.DateTime(nullable: false),
                        UserId = c.String(),
                        IpAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RealtyAds", t => t.RealtyAd_Id, cascadeDelete: true)
                .Index(t => t.RealtyAd_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RealtyAdImageDefaults", "RealtyAdImage_Id", "dbo.RealtyAdImages");
            DropForeignKey("dbo.RealtyAdImageDefaults", "RealtyAd_Id", "dbo.RealtyAds");
            DropForeignKey("dbo.RealtyAdPageViews", "RealtyAd_Id", "dbo.RealtyAds");
            DropForeignKey("dbo.RealtyAdMessages", "RealtyAd_Id", "dbo.RealtyAds");
            DropForeignKey("dbo.RealtyAdImages", "RealtyAd_Id", "dbo.RealtyAds");
            DropForeignKey("dbo.RealtyAds", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.RealtyAds", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.RealtyAdImageDefaults", new[] { "RealtyAdImage_Id" });
            DropIndex("dbo.RealtyAdImageDefaults", new[] { "RealtyAd_Id" });
            DropIndex("dbo.RealtyAdPageViews", new[] { "RealtyAd_Id" });
            DropIndex("dbo.RealtyAdMessages", new[] { "RealtyAd_Id" });
            DropIndex("dbo.RealtyAdImages", new[] { "RealtyAd_Id" });
            DropIndex("dbo.RealtyAds", new[] { "City_Id" });
            DropIndex("dbo.RealtyAds", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropTable("dbo.RealtyAdPageViews");
            DropTable("dbo.RealtyAdMessages");
            DropTable("dbo.RealtyAdImages");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.RealtyAds");
            DropTable("dbo.RealtyAdImageDefaults");
            DropTable("dbo.Cities");
        }
    }
}
