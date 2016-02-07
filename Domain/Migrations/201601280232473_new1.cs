using System.Data.Entity.Migrations;

namespace Domain.Migrations
{
    public partial class new1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fighters", "Division", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fighters", "Division");
        }
    }
}
