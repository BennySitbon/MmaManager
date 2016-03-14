namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fighterdivisioncanbenull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fighters", "Division", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fighters", "Division", c => c.Int(nullable: false));
        }
    }
}
