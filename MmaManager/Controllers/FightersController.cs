using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MmaManager.DAL;
using MmaManager.Models;
using Microsoft.AspNet.Identity;

namespace MmaManager.Controllers
{
    //[Authorize(Roles = "player")]
    [Authorize]
    public class FightersController : Controller
    {
        private readonly UfcContext db = new UfcContext();

        // GET: Fighters
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserName();
            return View(await db.Ownerships.Where(u => u.Username==userId).ToListAsync());
        }

        public decimal GetNetIncome(int ownershipId)
        {
            var ownership = db.Ownerships.Single(i => i.OwnershipID == ownershipId);
            var incoming = from trans in db.Transactions
                        where trans.ToUser == ownership.Username &&
                        ((trans.FightListing.BlueFighterFighterID == ownership.FighterID 
                        && trans.FightListing.FightResult == FightResult.BlueWin) ||
                        (trans.FightListing.RedFighterFighterID == ownership.FighterID &&
                        trans.FightListing.FightResult == FightResult.RedWin)) &&
                        trans.TimeStamp > ownership.Transaction.TimeStamp
                        select trans;

            decimal total = 0;
            foreach(var t in incoming)
            {
                total += t.Amount;
            }
            var outgoing = from trans in db.Transactions
                    where trans.FromUser == ownership.Username &&
                    trans.FighterID == ownership.FighterID
                    select trans;
            foreach (var t in outgoing)
            {
                total -= t.Amount;
            }
            return total;
        }
        public string GetOwnershipRecord(int ownershipID)
        {
            var wins = 0;
            var loses = 0;
            var draws = 0;
            var NC = 0;
            var ownership = db.Ownerships.Single(i => i.OwnershipID == ownershipID);
            var query = from listing in db.FightListings
                        where (listing.BlueFighterFighterID == ownership.FighterID ||
                            listing.RedFighterFighterID == ownership.FighterID) && 
                            listing.Event.Date > ownership.Transaction.TimeStamp
                        select listing;
            foreach(var f in query)
            {
                switch (f.FightResult)
                {
                    case FightResult.Draw:
                        draws++;
                        break;
                    case FightResult.NC:
                        NC++;
                        break;
                    default:
                        if (f.BlueFighterFighterID == ownership.FighterID && f.FightResult == FightResult.BlueWin)
                        {
                            wins++;
                        }
                        else if (f.RedFighterFighterID == ownership.FighterID && f.FightResult == FightResult.RedWin)
                        {
                            wins++;
                        }
                        else loses++;
                        break;
                }              
            }
            var result = wins+ "-" + loses;
            if (draws > 0) result = result + "-" + draws.ToString();
            if (NC > 0) result = result + " " + NC.ToString()+" NC";
            return result;
        }
        // GET: Fighters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fighter = await db.Fighters.FindAsync(id);
            
            if (fighter == null)
            {
                return HttpNotFound();
            }
            fighter.FightListings =
                await db.FightListings.Where(fl => fl.BlueFighterFighterID == id || fl.RedFighterFighterID == id).ToListAsync();
            return View(fighter);
        }

        // GET: Fighters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fighters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FighterId,FirstMidName,LastName,Nickname,Height,Reach,Ranking")] Fighter fighter)
        {
            if (ModelState.IsValid)
            {
                db.Fighters.Add(fighter);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fighter);
        }

        // GET: Fighters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fighter fighter = await db.Fighters.FindAsync(id);
            if (fighter == null)
            {
                return HttpNotFound();
            }
            return View(fighter);
        }

        // POST: Fighters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FighterId,FirstMidName,LastName,Nickname,Height,Reach,Ranking")] Fighter fighter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fighter).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fighter);
        }

        // GET: Fighters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fighter fighter = await db.Fighters.FindAsync(id);
            if (fighter == null)
            {
                return HttpNotFound();
            }
            return View(fighter);
        }

        // POST: Fighters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Fighter fighter = await db.Fighters.FindAsync(id);
            db.Fighters.Remove(fighter);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
