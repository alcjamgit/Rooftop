namespace RealEstateApp.MigrationsAppDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RealtyAds",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ShortDescn = c.String(),
                        LongDescn = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RealtyAds");
        }
    }
}
