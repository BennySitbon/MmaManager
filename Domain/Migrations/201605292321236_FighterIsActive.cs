namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FighterIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fighters", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fighters", "IsActive");
        }
    }
}
