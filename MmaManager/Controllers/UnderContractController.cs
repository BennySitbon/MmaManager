using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.DAL;
using Domain.Models;
using Microsoft.AspNet.Identity;
using MmaManager.ViewModels;
using Service.Entity;

namespace MmaManager.Controllers
{
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
                i.OwnershipRecord = _ownershipService.GetOwnershipFightRecord(i.Ownership.OwnershipID);
            });
            //TODO: put recommended prices in the model?
            return View(m);
        }

        public ActionResult Details(int id)
        {
            //TODO: place ownership details under fighter details
            var fighter = _repository.Get<Fighter>(id, true);
            if (fighter == null)
            {
                return HttpNotFound();
            }

            return View(fighter);
        }
    }
}
