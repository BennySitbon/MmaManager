using System.Net;
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
        public ActionResult Create(int? eventId = null)
        {
            ViewBag.BlueFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullName");
            ViewBag.EventID = eventId != null
                ? new SelectList(_repository.GetAll<Event>(), "EventID", "Name", eventId)
                : new SelectList(_repository.GetAll<Event>(), "EventID", "Name");
            
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
            ViewBag.RedFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullName", fightListing.RedFighterFighterID);
            return View(fightListing);
        }

        // GET: FightListings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FightListing fightListing = _repository.Get<FightListing>(id.Value);
            if (fightListing == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlueFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullName",
                fightListing.BlueFighterFighterID);
            ViewBag.EventID = new SelectList(_repository.GetAll<Event>(), "EventID", "Name", fightListing.EventID);
            ViewBag.RedFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullName",
                fightListing.RedFighterFighterID);
            return View(fightListing);
        }

        // POST: FightListings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FightListingID,RedFighterFighterID,BlueFighterFighterID,EventID,FightResult,WinRound,WinTime,WinType,FightBonus")] FightListing fightListing)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(fightListing);
                return RedirectToAction("Index","Events");
            }
            ViewBag.BlueFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullName",
                fightListing.BlueFighterFighterID);
            ViewBag.EventID = new SelectList(_repository.GetAll<Event>(), "EventID", "Name", fightListing.EventID);
            ViewBag.RedFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullName",
                fightListing.RedFighterFighterID);
            return View(fightListing);
        }
        
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

    }
}
