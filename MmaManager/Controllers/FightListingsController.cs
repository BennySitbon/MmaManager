using System.Web.Mvc;
using Domain.DAL;
using Domain.Models;

namespace MmaManager.Controllers
{
    public class FightListingsController : Controller
    {
        private readonly IRepository _repository;
        public FightListingsController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View(_repository.GetAll<FightListing>());
        }

        public ActionResult Details(int id)
        {
            var fightListing = _repository.Get<FightListing>(id);
            if (fightListing == null)
            {
                return HttpNotFound();
            }
            return View(fightListing);
        }

        // GET: FightListings/Create
        public ActionResult Create()
        {
            ViewBag.BlueFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullName");
            ViewBag.EventID = new SelectList(_repository.GetAll<Event>(), "EventID", "Name");
            ViewBag.RedFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullName");
            return View();
        }

        // POST: FightListings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RedFighterFighterID,BlueFighterFighterID,EventID,FightResult,WinRound,WinTime,WinType,FightBonus")] FightListing fightListing)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(fightListing);
                return RedirectToAction("Index","Events");
            }

            ViewBag.BlueFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullName", fightListing.BlueFighterFighterID);
            ViewBag.EventID = new SelectList(_repository.GetAll<Event>(), "EventID", "Name", fightListing.EventID);
            ViewBag.RedFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullName", fightListing.BlueFighterFighterID);
            return View(fightListing);
        }

    //    // GET: FightListings/Edit/5
    //    public async Task<ActionResult> Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        FightListing fightListing = await db.FightListings.FindAsync(id);
    //        if (fightListing == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        ViewBag.BlueFighterFighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName", fightListing.BlueFighterFighterID);
    //        ViewBag.EventID = new SelectList(db.Events, "EventID", "Name", fightListing.EventID);
    //        ViewBag.RedFighterFighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName", fightListing.RedFighterFighterID);
    //        return View(fightListing);
    //    }

    //    // POST: FightListings/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<ActionResult> Edit([Bind(Include = "FightListingID,RedFighterFighterID,BlueFighterFighterID,EventID,FightResult,WinRound,WinTime,WinType,FightBonus")] FightListing fightListing)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Entry(fightListing).State = EntityState.Modified;
    //            await db.SaveChangesAsync();
    //            return RedirectToAction("Index");
    //        }
    //        ViewBag.BlueFighterFighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName", fightListing.BlueFighterFighterID);
    //        ViewBag.EventID = new SelectList(db.Events, "EventID", "Name", fightListing.EventID);
    //        ViewBag.RedFighterFighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName", fightListing.RedFighterFighterID);
    //        return View(fightListing);
    //    }

    //    // GET: FightListings/Delete/5
    //    public async Task<ActionResult> Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        FightListing fightListing = await db.FightListings.FindAsync(id);
    //        if (fightListing == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(fightListing);
    //    }

    //    // POST: FightListings/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<ActionResult> DeleteConfirmed(int id)
    //    {
    //        FightListing fightListing = await db.FightListings.FindAsync(id);
    //        db.FightListings.Remove(fightListing);
    //        await db.SaveChangesAsync();
    //        return RedirectToAction("Index");
    //    }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
