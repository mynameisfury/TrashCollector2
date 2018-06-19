namespace TrashCollector2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableints : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pickups", "WorkerID", "dbo.Workers");
            DropIndex("dbo.Pickups", new[] { "WorkerID" });
            AlterColumn("dbo.Pickups", "WorkerID", c => c.Int());
            AlterColumn("dbo.Pickups", "SuspensionDate", c => c.DateTime());
            CreateIndex("dbo.Pickups", "WorkerID");
            AddForeignKey("dbo.Pickups", "WorkerID", "dbo.Workers", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pickups", "WorkerID", "dbo.Workers");
            DropIndex("dbo.Pickups", new[] { "WorkerID" });
            AlterColumn("dbo.Pickups", "SuspensionDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pickups", "WorkerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Pickups", "WorkerID");
            AddForeignKey("dbo.Pickups", "WorkerID", "dbo.Workers", "ID", cascadeDelete: true);
        }
    }
}
