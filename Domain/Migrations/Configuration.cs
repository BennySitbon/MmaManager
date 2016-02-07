using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Domain.DAL;
using Domain.Models;
using Domain.Models.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UfcContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MmaManager.DAL.UfcContext";
            //if (System.Diagnostics.Debugger.IsAttached == false) System.Diagnostics.Debugger.Launch();
        }

        protected override void Seed(UfcContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            roleManager.Create(new IdentityRole { Name = "admin" });
            roleManager.Create(new IdentityRole { Name = "customer" });

            var adminId = "admin@MmaManager.com";
            var userId = "user@example.com";

            if (!(context.Users.Any(u => u.Email == adminId)))
            {
                var user = new ApplicationUser { UserName = adminId, Email = adminId };
                userManager.Create(user, "P@ssword1");
                userManager.AddToRole(user.Id, "admin");
            }

            if (!(context.Users.Any(u => u.Email == userId)))
            {
                var user = new ApplicationUser { UserName = userId, Email = userId };
                userManager.Create(user, "P@ssword1");
                userManager.AddToRole(user.Id, "customer");
                context.Transactions.Add(new Transaction()
                {
                    Amount = 15000,
                    FromUser = adminId,
                    ToUser = userId,
                    TimeStamp = DateTime.UtcNow,
                    TransactionType = TransactionType.NewPlayer
                });
            }
            context.SaveChanges();
            

            var fighters = new List<Fighter>
            {
                new Fighter
                {
                    FirstMidName = "Andrei",LastName="Arlovski",Height = 76,Nickname="The Pitbull",Wins = 20,Loses = 9
                    ,Division = Division.Heavyweight
                },
                new Fighter { FirstMidName = "Frank",LastName="Mir",Wins = 22,Loses = 14},
                new Fighter { FirstMidName = "John",LastName="Dodson",Height=63,Reach= 64,Nickname="The Magician"
                    ,Division = Division.Flyweight},
                new Fighter { FirstMidName = "Demetrious",LastName="Johnson",Height=63,Reach= 64,Nickname="Mighty Mouse"},
                new Fighter { FirstMidName = "Anthony",LastName="Johnson",Nickname="Rumble"},
                new Fighter { FirstMidName = "Jimi",LastName="Manuwa",Height=73,Reach= 80,Nickname="Poster Boy"}

            };
            fighters.ForEach(s => context.Fighters.AddOrUpdate(p => new { p.LastName, p.FirstMidName }, s));
            context.SaveChanges();
            var events = new List<Event>
            {
                new Event{
                    Date = DateTime.Parse("2015-09-05"),
                    FightCard = new List<FightListing>(),
                    Name = "UFC 191"
                }
            };
            events.ForEach(s => context.Events.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var listings = new List<FightListing>
            {
                new FightListing {
                    BlueFighterFighterID = fighters.Single(s => s.LastName == "Arlovski").FighterId,
                    RedFighterFighterID = fighters.Single(s => s.LastName == "Mir").FighterId,
                    FightResult = FightResult.BlueWin,
                    WinType = WinType.Decision,
                    WinRound = 3,
                    WinTime = 5,
                    EventID = events.Single(s=>s.Name=="UFC 191").EventID
                },
                new FightListing{
                    BlueFighterFighterID = fighters.Single(s => s.LastName == "Manuwa").FighterId,
                    RedFighterFighterID = fighters.Single(s => s.Nickname == "Rumble").FighterId,
                    FightResult = FightResult.RedWin,
                    WinType = WinType.KO,
                    WinRound = 2,
                    WinTime = 1,
                    EventID = events.Single(s=>s.Name=="UFC 191").EventID
                },
                new FightListing{
                    BlueFighterFighterID = fighters.Single(s => s.LastName == "Dodson").FighterId,
                    RedFighterFighterID = fighters.Single(s => s.Nickname == "Mighty Mouse").FighterId,
                    FightResult = FightResult.RedWin,
                    WinType = WinType.Decision,
                    WinRound = 5,
                    WinTime = 5,
                    EventID = events.Single(s=>s.Name=="UFC 191").EventID
                }
            };
            listings.ForEach(s => context.FightListings.AddOrUpdate(p => new { p.BlueFighterFighterID, p.EventID }, s));
            context.SaveChanges();
            var transactions = new List<Transaction>
            {
                new Transaction{
                    TimeStamp = DateTime.Parse("2015-09-1 15:00:00"),
                    TransactionType = TransactionType.Sell,
                    FromUser = userId,
                    ToUser = adminId,
                    Amount = 5000,
                    FighterID = fighters.Single(s => s.LastName=="Arlovski").FighterId
                },
                new Transaction{
                    TimeStamp = DateTime.Parse("2015-09-21 14:00:00"),
                    TransactionType = TransactionType.Sell,
                    FromUser = adminId,
                    ToUser = adminId,
                    Amount = 0,
                    FighterID = fighters.Single(s => s.LastName=="Mir").FighterId
                },
                new Transaction{
                        TimeStamp = DateTime.Parse("2015-09-21 15:00:00"),
                        TransactionType = TransactionType.Sell,
                        FromUser = adminId,
                        ToUser = adminId,
                        Amount = 4000,
                        FighterID = fighters.Single(s => s.LastName=="Dodson").FighterId
                },
                new Transaction{
                    TimeStamp = DateTime.Parse("2015-09-06 16:00:00"),
                    TransactionType = TransactionType.Winnings,
                    FromUser = adminId,
                    ToUser = userId,
                    Amount = 1000,
                    FightListingID = listings.First(s=>s.BlueFighterFighterID==fighters.Single(i=>i.LastName == "Arlovski").FighterId).FightListingID
                }
            };
            transactions.ForEach(s => context.Transactions.AddOrUpdate(p => new { p.FromUser, p.ToUser, p.Amount, p.TimeStamp }, s));
            context.SaveChanges();

            var ownerships = new List<Ownership>
            {
                new Ownership{ 
                    Username= userId,
                    FighterID=fighters.SingleOrDefault(s=>s.LastName=="Arlovski").FighterId,
                    TransactionID = transactions.Where(s=>s.FighterID==1 &&
                        s.FromUser== userId
                        ).SingleOrDefault().TransactionID
                },
                new Ownership{ 
                    Username= adminId,
                    FighterID=fighters.SingleOrDefault(s=>s.LastName=="Dodson").FighterId,
                    TransactionID = transactions.Where(s=>s.FighterID==3 &&
                        s.FromUser==adminId
                        ).SingleOrDefault().TransactionID,
                    PriceRequested=12000
                },
                new Ownership{
                    Username= adminId,
                    FighterID=fighters.SingleOrDefault(s=>s.LastName=="Mir").FighterId,
                    TransactionID=transactions.Where(s=>s.FighterID==2 && s.FromUser==adminId).SingleOrDefault().TransactionID,
                    PriceRequested=2000
                }
            };
            ownerships.ForEach(s => context.Ownerships.AddOrUpdate(p => new { p.FighterID, p.Username }, s));
            context.SaveChanges();
        }
    }
}
