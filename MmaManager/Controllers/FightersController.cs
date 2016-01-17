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
using MmaManager.Service;

namespace MmaManager.Controllers
{
    //[Authorize(Roles = "player")]
    [Authorize]
    public class FightersController : Controller
    {

        private readonly FighterService _fighterService = new FighterService( new Repository());
        private readonly OwnershipService _ownershipService = new OwnershipService(new Repository());

        // GET: Fighters
        public ActionResult Index()
        {            
            if (User.IsInRole("admin"))
            {
                return View(_ownershipService.GetAllAsList());
            }
            var userName = User.Identity.GetUserName();
            return View(_ownershipService.GetOwnershipListForUser(userName).ToList());
        }

        public decimal GetNetIncome(int ownershipId)
        {
            return _ownershipService.GetNetIncome(ownershipId);
        }
        public string GetOwnershipRecord(int ownershipId)
        {
            return _ownershipService.GetOwnershipRecord(ownershipId);
        }
        // GET: Fighters/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var fighter = _fighterService.GetLoaded(id);
            if (fighter == null)
            {
                return HttpNotFound();
            }

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
                //db.GetAllFighters().Add(fighter);
                _fighterService.Add(fighter);
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fighter);
        }

        // GET: Fighters/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            //Fighter fighter = await db.Fighters.FindAsync(id);
            var fighter = _fighterService.Get(id);
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
            /*if (ModelState.IsValid)
            {
                db.Entry(fighter).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }*/
            //TODO: make it update through the service
            return View(fighter);
        }

        // GET: Fighters/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            Fighter fighter = _fighterService.Get(id);//db.Fighters.FindAsync(id);
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
            /*Fighter fighter = await db.Fighters.FindAsync(id);
            db.Fighters.Remove(fighter);
            await db.SaveChangesAsync();*/
            //TODO: make delete through the service
            return RedirectToAction("Index");
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
