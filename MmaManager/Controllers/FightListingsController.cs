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
using MmaManager.Service;

namespace MmaManager.Controllers
{
    public class FightListingsController : Controller
    {
        //private readonly FightListingService _fightListingService;
        private readonly IRepository _repository = new Repository();
        public FightListingsController()
        {
            //_fightListingService = new FightListingService(new Repository());
        }
        // GET: FightListings
        public async Task<ActionResult> Index()
        {
            //var fightListings = _fightListingService.GetAllAsList();
            //return View(fightListings);
            return View(_repository.GetAll<FightListing>());
        }

        // GET: FightListings/Details/5
        public ActionResult Details(int id)
        {
            //var fightListing = _fightListingService.GetLoaded(id);
            var fightListing = _repository.Get<FightListing>(id);
            if (fightListing == null)
            {
                return HttpNotFound();
            }
            return View(fightListing);
        }

        // GET: FightListings/Create
        /*public ActionResult Create()
        {
            /*ViewBag.BlueFighterFighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName");
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Name");
            ViewBag.RedFighterFighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName");
            return View();
        }

        // POST: FightListings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FightListingID,RedFighterFighterID,BlueFighterFighterID,EventID,FightResult,WinRound,WinTime,WinType,FightBonus")] FightListing fightListing)
        {
            if (ModelState.IsValid)
            {
                db.FightListings.Add(fightListing);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BlueFighterFighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName", fightListing.BlueFighterFighterID);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Name", fightListing.EventID);
            ViewBag.RedFighterFighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName", fightListing.RedFighterFighterID);
            return View(fightListing);
        }

        // GET: FightListings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FightListing fightListing = await db.FightListings.FindAsync(id);
            if (fightListing == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlueFighterFighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName", fightListing.BlueFighterFighterID);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Name", fightListing.EventID);
            ViewBag.RedFighterFighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName", fightListing.RedFighterFighterID);
            return View(fightListing);
        }

        // POST: FightListings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FightListingID,RedFighterFighterID,BlueFighterFighterID,EventID,FightResult,WinRound,WinTime,WinType,FightBonus")] FightListing fightListing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fightListing).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BlueFighterFighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName", fightListing.BlueFighterFighterID);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Name", fightListing.EventID);
            ViewBag.RedFighterFighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName", fightListing.RedFighterFighterID);
            return View(fightListing);
        }

        // GET: FightListings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FightListing fightListing = await db.FightListings.FindAsync(id);
            if (fightListing == null)
            {
                return HttpNotFound();
            }
            return View(fightListing);
        }

        // POST: FightListings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FightListing fightListing = await db.FightListings.FindAsync(id);
            db.FightListings.Remove(fightListing);
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
        }*/
    }
}
