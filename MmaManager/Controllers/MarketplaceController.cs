using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MmaManager.DAL;
using MmaManager.Models;

namespace MmaManager.Controllers
{
    public class MarketplaceController : Controller
    {
        private UfcContext db = new UfcContext();

        // GET: Marketplace
        public async Task<ActionResult> Index()
        {
            var query = from ownership in db.Ownerships
                        where ownership.PriceRequested > 0 
                        select ownership;            
            return View(await query.ToListAsync());
        }
        public async Task<ActionResult> BuyFighter(int ownershipId, string username)
        {
            if (ownershipId == 0 || username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fighterId = db.Ownerships.Single(f => f.OwnershipID == ownershipId).FighterID;
            var transactionToSave = new Transaction
            {
                FromUser = username,
                ToUser = "admin@MmaManager.com",
                Amount = db.Ownerships.Single(u => u.OwnershipID==ownershipId && u.FighterID.Equals(fighterId)).PriceRequested,
                TimeStamp = DateTime.Now,
                TransactionType = TransactionType.Sell,
                FighterID = fighterId
            };
            db.Transactions.Add(transactionToSave);
            var ownershipToSave = new Ownership { FighterID = fighterId, Username = username, TransactionID = transactionToSave.TransactionID };
            db.Ownerships.Add(ownershipToSave);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Fighters");
        }
        // GET: Marketplace/Details/5
        public async Task<ActionResult> Details(int? id)
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

        // GET: Marketplace/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marketplace/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FighterId,FirstMidName,LastName,Nickname,Height,reach,Ranking")] Fighter fighter)
        {
            if (ModelState.IsValid)
            {
                db.Fighters.Add(fighter);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fighter);
        }

        // GET: Marketplace/Edit/5
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

        // POST: Marketplace/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FighterId,FirstMidName,LastName,Nickname,Height,reach,Ranking")] Fighter fighter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fighter).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fighter);
        }

        // GET: Marketplace/Delete/5
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

        // POST: Marketplace/Delete/5
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
