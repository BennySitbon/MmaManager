namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DrawsContest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fighters", "Draws", c => c.Int(nullable: false));
            AddColumn("dbo.Fighters", "NoContest", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fighters", "NoContest");
            DropColumn("dbo.Fighters", "Draws");
        }
    }
}
