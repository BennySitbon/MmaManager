using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.DAL;
using Domain.Models;
using Microsoft.AspNet.Identity;
using MmaManager.ViewModels;
using Service.Entity;

namespace MmaManager.Controllers
{
    //[Authorize(Roles = "player")]
    //TODO: Rename this
    [Authorize]
    public class UnderContractController : Controller
    {
        private readonly IRepository _repository;
        private readonly IOwnershipService _ownershipService;

        public UnderContractController(IRepository repository,IOwnershipService ownershipService)
        {
            _repository = repository;
            _ownershipService = ownershipService;
        }

        public ActionResult Index()
        {
            List<OwnershipViewModel> m;
            if (User.IsInRole("admin"))
            {
                //return View(_repository.GetAll<Ownership>());
                m = _repository.GetAll<Ownership>().Select(i => new OwnershipViewModel {Ownership = i}).ToList();
            }
            else
            {
                var userName = User.Identity.GetUserName();
                m = _repository.GetAll<Ownership>(o => o.Where(i => i.Username == userName))
                        .Select(i => new OwnershipViewModel {Ownership = i}).ToList();                
            }
            m.ForEach(i =>
            {
                i.NetIncome = _ownershipService.GetNetIncome(i.Ownership.OwnershipID);
                i.OwnershipRecord = _ownershipService.GetOwnershipRecord(i.Ownership.OwnershipID);
            });
            //var userName = User.Identity.GetUserName();
            //return View(_repository.GetAll<Ownership>(o => o.Where(i => i.Username == userName).ToList()));
            return View(m);
        }

        //public decimal GetNetIncome(int ownershipId)
        //{
        //    return _ownershipService.GetNetIncome(ownershipId);
        //}
        //public string GetOwnershipRecord(int ownershipId)
        //{
        //    return _ownershipService.GetOwnershipRecord(ownershipId);
        //}
        // GET: Fighters/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //var fighter = _fighterService.GetLoaded(id);     
            //TODO: place ownership details under fighter details
            var fighter = _repository.Get<Fighter>(id, true);
            if (fighter == null)
            {
                return HttpNotFound();
            }

            return View(fighter);
        }

        //public ActionResult PutForSale(int ownershipId, decimal priceRequested)
        //{
        //    //TODO: Finalize the view
        //    _ownershipService.PutOwnershipForSale(ownershipId,priceRequested);
        //    return RedirectToAction("Index");
        //}
    }
}
