using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using DataImporter;
using Domain.DAL;
using Domain.Models;

namespace MmaManager.Controllers
{
    [Authorize(Roles="admin")]
    public class FightersManagementController : Controller
    {
        private readonly IRepository _repository;
        public FightersManagementController(IRepository repository)
        {
            _repository = repository;
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

        public string RunImport()
        {
            var result = FighterImporter.GetGoogleSheetData((row,worksheet) =>
            {
                var fighter = new Fighter();
                int success;
                if (Int32.TryParse(row.Elements[0].Value, out success))
                {
                    fighter.Ranking = success;
                }
                Regex rgx = new Regex("[^a-zA-Z ]");
                var dirtyName = row.Elements[2].Value;
                var fullName = rgx.Replace(dirtyName, "").Split(' ');
                var lastNameList = fullName.Skip(1).ToList();
                var lastNameTemp = "";
                lastNameList.ForEach(l =>
                {
                    lastNameTemp = lastNameTemp + " " + l;
                });
                fighter.LastName = lastNameTemp.TrimStart();
                fighter.FirstMidName = fullName[0];
                if (row.Elements[0].Value == "C.")
                {
                    fighter.Ranking = 0;
                }
                else if (Int32.TryParse(row.Elements[0].Value, out success))
                {
                    fighter.Ranking = success;
                }
                if (Int32.TryParse(row.Elements[5].Value, out success))
                {
                    fighter.Wins = success;
                }
                if (Int32.TryParse(row.Elements[6].Value, out success))
                {
                    fighter.Loses = success;
                }
                if (Int32.TryParse(row.Elements[10].Value, out success))
                {
                    fighter.Draws = success;
                }
                if (Int32.TryParse(row.Elements[11].Value, out success))
                {
                    fighter.NoContest = success;
                }
                Division div;
                div = Enum.TryParse(worksheet.Title.Text, true, out div) ? div : Division.Unknown;
                if (div != Division.Unknown)
                {
                    fighter.Division = div;
                }
                return fighter;
            });
            _repository.Add(result[0]);
            return System.Web.Helpers.Json.Encode(result);
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
        public ActionResult Edit([Bind(Include = "FighterId,FirstMidName,LastName,Nickname,Height,Reach,Ranking")] Fighter fighter)
        {
            if (ModelState.IsValid)
            {                
                _repository.Update(fighter);
                return RedirectToAction("Index");
            }
            return View(fighter);
        }

        // GET: FightersManagement/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: FightersManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //var fighter = _fighterService.Get(id);
            //_fighterService.Remove(fighter);
            var fighter = _repository.Get<Fighter>(id);
            _repository.Delete(fighter);
            return RedirectToAction("Index");
        }
    }
}
