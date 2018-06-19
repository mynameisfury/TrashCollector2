namespace TrashCollector2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latandlongadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Addresses", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "Longitude");
            DropColumn("dbo.Addresses", "Latitude");
        }
    }
}
