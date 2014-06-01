namespace RealEstateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIdentityTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DateJoined", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "ProfilePhotoFileName", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ProfilePhotoFileName");
            DropColumn("dbo.AspNetUsers", "DateJoined");
        }
    }
}
