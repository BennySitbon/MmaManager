using System.Linq;
using System.Web.Mvc;
using Service.Entity;

namespace MmaManager.Controllers
{
    public class MarketplaceController : Controller
    {
        private readonly IMarketplaceService _marketplaceService;

        public MarketplaceController(IMarketplaceService marketplaceService)
        {
            _marketplaceService = marketplaceService;
        }

        // GET: Marketplace
        public ActionResult Index(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) return View(_marketplaceService.GetAllOnSaleOwnershipsList());

            return
                View(
                    _marketplaceService.GetAllOnSaleOwnershipsList()
                        .Where(i => i.Fighter.FullName.ToLower().Contains(searchString.ToLower()))
                        .ToList());
        }

        public ActionResult BuyFighter(int ownershipId)
        {
            _marketplaceService.BuyFighter(ownershipId);
            return RedirectToAction("Index","UnderContract");
        }
        // GET: Marketplace/Details/5
        /*public async Task<ActionResult> Details(int? id)
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
        }*/
    }
}
