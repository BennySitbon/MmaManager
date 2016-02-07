using System.Data.Entity.Migrations;

namespace Domain.Migrations
{
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventID);
            
            CreateTable(
                "dbo.FightListings",
                c => new
                    {
                        FightListingID = c.Int(nullable: false, identity: true),
                        RedFighterFighterID = c.Int(nullable: false),
                        BlueFighterFighterID = c.Int(nullable: false),
                        EventID = c.Int(nullable: false),
                        FightResult = c.Int(),
                        WinRound = c.Int(),
                        WinTime = c.Int(),
                        WinType = c.Int(),
                        FightBonus = c.Int(),
                        Fighter_FighterId = c.Int(),
                    })
                .PrimaryKey(t => t.FightListingID)
                .ForeignKey("dbo.Fighters", t => t.Fighter_FighterId)
                .ForeignKey("dbo.Fighters", t => t.BlueFighterFighterID)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .ForeignKey("dbo.Fighters", t => t.RedFighterFighterID)
                .Index(t => t.RedFighterFighterID)
                .Index(t => t.BlueFighterFighterID)
                .Index(t => t.EventID)
                .Index(t => t.Fighter_FighterId);
            
            CreateTable(
                "dbo.Fighters",
                c => new
                    {
                        FighterId = c.Int(nullable: false, identity: true),
                        FirstMidName = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Nickname = c.String(maxLength: 30),
                        Height = c.Int(),
                        Reach = c.Int(),
                        Ranking = c.Int(),
                        Wins = c.Int(nullable: false),
                        Loses = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FighterId);
            
            CreateTable(
                "dbo.Ownerships",
                c => new
                    {
                        OwnershipID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        FighterID = c.Int(nullable: false),
                        TransactionID = c.Int(nullable: false),
                        PriceRequested = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.OwnershipID)
                .ForeignKey("dbo.Fighters", t => t.FighterID, cascadeDelete: true)
                .ForeignKey("dbo.Transactions", t => t.TransactionID, cascadeDelete: true)
                .Index(t => t.FighterID)
                .Index(t => t.TransactionID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        FromUser = c.String(),
                        ToUser = c.String(),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        TimeStamp = c.DateTime(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        FightListingID = c.Int(),
                        FighterID = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.Fighters", t => t.FighterID)
                .ForeignKey("dbo.FightListings", t => t.FightListingID)
                .Index(t => t.FightListingID)
                .Index(t => t.FighterID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Ownerships", "TransactionID", "dbo.Transactions");
            DropForeignKey("dbo.Transactions", "FightListingID", "dbo.FightListings");
            DropForeignKey("dbo.Transactions", "FighterID", "dbo.Fighters");
            DropForeignKey("dbo.Ownerships", "FighterID", "dbo.Fighters");
            DropForeignKey("dbo.FightListings", "RedFighterFighterID", "dbo.Fighters");
            DropForeignKey("dbo.FightListings", "EventID", "dbo.Events");
            DropForeignKey("dbo.FightListings", "BlueFighterFighterID", "dbo.Fighters");
            DropForeignKey("dbo.FightListings", "Fighter_FighterId", "dbo.Fighters");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Transactions", new[] { "FighterID" });
            DropIndex("dbo.Transactions", new[] { "FightListingID" });
            DropIndex("dbo.Ownerships", new[] { "TransactionID" });
            DropIndex("dbo.Ownerships", new[] { "FighterID" });
            DropIndex("dbo.FightListings", new[] { "Fighter_FighterId" });
            DropIndex("dbo.FightListings", new[] { "EventID" });
            DropIndex("dbo.FightListings", new[] { "BlueFighterFighterID" });
            DropIndex("dbo.FightListings", new[] { "RedFighterFighterID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Transactions");
            DropTable("dbo.Ownerships");
            DropTable("dbo.Fighters");
            DropTable("dbo.FightListings");
            DropTable("dbo.Events");
        }
    }
}
