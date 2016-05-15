using System.Net;
using System.Web.Mvc;
using Domain.DAL;
using Domain.Models;
using Service.Entity;

namespace MmaManager.Controllers
{
    public class FightListingsController : Controller
    {
        private readonly IRepository _repository;
        private readonly IFightListingService _fightListingService;

        public FightListingsController(IRepository repository, IFightListingService fightListingService)
        {
            _repository = repository;
            _fightListingService = fightListingService;
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
        [Authorize(Roles = "admin")]
        // GET: FightListings/Create
        public ActionResult Create(int? eventId = null)
        {
            ViewBag.BlueFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullNameWithNickname");
            ViewBag.EventID = eventId != null
                ? new SelectList(_repository.GetAll<Event>(), "EventID", "Name", eventId)
                : new SelectList(_repository.GetAll<Event>(), "EventID", "Name");
            
            ViewBag.RedFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullNameWithNickname");
            return View();
        }

        // POST: FightListings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RedFighterFighterID,BlueFighterFighterID" +
                                                   ",EventID,FightResult,WinRound,WinTime," +
                                                   "WinType,FightBonus")] FightListing fightListing)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(fightListing);
                _fightListingService.PayOwners(fightListing);
                return RedirectToAction("Index","Events");
            }

            var allFighters = _repository.GetAll<Fighter>();
            ViewBag.BlueFighterFighterId = new SelectList(allFighters, "FighterId", "FullNameWithNickname",
                fightListing.BlueFighterFighterID);

            ViewBag.EventID = new SelectList(_repository.GetAll<Event>(), "EventID", "Name", fightListing.EventID);

            ViewBag.RedFighterFighterId = new SelectList(allFighters, "FighterId", "FullNameWithNickname",
                fightListing.RedFighterFighterID);

            return View(fightListing);
        }

        [Authorize(Roles = "admin")]
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
            ViewBag.BlueFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullNameWithNickname",
                fightListing.BlueFighterFighterID);

            ViewBag.EventID = new SelectList(_repository.GetAll<Event>(), "EventID", "Name", fightListing.EventID);

            ViewBag.RedFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullNameWithNickname",
                fightListing.RedFighterFighterID);

            return View(fightListing);
        }
        [Authorize(Roles = "admin")]
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
            ViewBag.BlueFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullNameWithNickname",
                fightListing.BlueFighterFighterID);
            ViewBag.EventID = new SelectList(_repository.GetAll<Event>(), "EventID", "Name", fightListing.EventID);
            ViewBag.RedFighterFighterId = new SelectList(_repository.GetAll<Fighter>(), "FighterId", "FullNameWithNickname",
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
