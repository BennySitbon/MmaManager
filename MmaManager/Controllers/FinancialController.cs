using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using MmaManager.DAL;
using MmaManager.Models;

namespace MmaManager.Controllers
{
    public class FinancialController : Controller
    {
        private UfcContext db = new UfcContext();

        // GET: Financial
        public async Task<ActionResult> Index()
        {
            var transactions = db.Transactions.Include(t => t.Fighter).Include(t => t.FightListing).Where(t=>t.FromUser==User.Identity.Name|| t.ToUser==User.Identity.Name);
            return View(await transactions.ToListAsync());
        }

        // GET: Financial/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Financial/Create
        public ActionResult Create()
        {
            ViewBag.FighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName");
            ViewBag.FightListingID = new SelectList(db.FightListings, "FightListingID", "FightListingID");
            return View();
        }

        // POST: Financial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TransactionID,FromUserID,TargetUserID,Amount,TimeStamp,TransactionType,FightListingID,FighterId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName", transaction.FighterID);
            ViewBag.FightListingID = new SelectList(db.FightListings, "FightListingID", "FightListingID", transaction.FightListingID);
            return View(transaction);
        }

        // GET: Financial/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.FighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName", transaction.FighterID);
            ViewBag.FightListingID = new SelectList(db.FightListings, "FightListingID", "FightListingID", transaction.FightListingID);
            return View(transaction);
        }

        // POST: Financial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TransactionID,FromUserID,TargetUserID,Amount,TimeStamp,TransactionType,FightListingID,FighterId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FighterID = new SelectList(db.Fighters, "FighterId", "FirstMidName", transaction.FighterID);
            ViewBag.FightListingID = new SelectList(db.FightListings, "FightListingID", "FightListingID", transaction.FightListingID);
            return View(transaction);
        }

        // GET: Financial/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Financial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Transaction transaction = await db.Transactions.FindAsync(id);
            db.Transactions.Remove(transaction);
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
