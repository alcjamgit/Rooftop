namespace RealEstateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileNameToDefaultImg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RealtyAdImageDefaults", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RealtyAdImageDefaults", "FileName");
        }
    }
}
