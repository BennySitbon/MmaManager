using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using DataImporter;
using Domain.DAL;
using Domain.Models;
using Service.Administration;

namespace MmaManager.Controllers
{
    [Authorize(Roles="admin")]
    public class FightersManagementController : Controller
    {
        private readonly IRepository _repository;
        private readonly IFighterImportService _fighterImportService;

        public FightersManagementController(IRepository repository, IFighterImportService fighterImportService)
        {
            _repository = repository;
            _fighterImportService = fighterImportService;
        }

        // GET: FightersManagement
        public ActionResult Index(string searchString)
        {
            //return View(_fighterService.GetAllAsList());
            if (string.IsNullOrEmpty(searchString)) return View(_repository.GetAll<Fighter>());
            var searchS = searchString.ToLower();
            return View(_repository.GetAll<Fighter>(fighter => fighter.Where(i =>
                i.FirstMidName.ToLower().Contains(searchS)
                || i.LastName.ToLower().Contains(searchS)
                || i.Nickname.ToLower().Contains(searchS))));
        }

        // GET: FightersManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FightersManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FighterId,FirstMidName,LastName,Nickname,Height,Reach,Ranking,Wins,Loses,Division,Draws,NoContest")] Fighter fighter)
        {
            if (!ModelState.IsValid) return View(fighter);

            _repository.Add(fighter);
            return RedirectToAction("Index");
        }

        // GET: FightersManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fighter = _repository.Get<Fighter>(id.Value);
            if (fighter == null)
            {
                return HttpNotFound();
            }
            return View(fighter);
        }

        // POST: FightersManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FighterId,FirstMidName,LastName,Nickname," +
                          "Height,Reach,Ranking,Division,Wins,Loses,Draws,NoContest")] Fighter fighter)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(fighter);
                return RedirectToAction("Index");
            }
            return View(fighter);
        }

        //public ActionResult ImportAllFighters()
        //{
        //    var allFighters = _fighterImportService.GetFightersFromImport();

        //}
        // GET: FightersManagement/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var fighter = _repository.Get<Fighter>(id.Value);
        //    if (fighter == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(fighter);
        //}

        //// POST: FightersManagement/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    //var fighter = _fighterService.Get(id);
        //    //_fighterService.Remove(fighter);
        //    var fighter = _repository.Get<Fighter>(id);
        //    _repository.Delete(fighter);
        //    return RedirectToAction("Index");
        //}
    }
}
