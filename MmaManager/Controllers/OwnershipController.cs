using System.Web.Mvc;
using Domain.DAL;
using Domain.Models;
using Service.Entity;

namespace MmaManager.Controllers
{
    public class OwnershipController : Controller
    {
        private readonly IOwnershipService _ownershipService;
        private readonly IRepository _repository;

        public OwnershipController(IOwnershipService ownershipService,IRepository repository)
        {
            _ownershipService = ownershipService;
            _repository = repository;
        }

        public decimal GetNetIncome(int ownershipId)
        {
            return _ownershipService.GetNetIncome(ownershipId);
        }

        public string GetOwnershipRecord(int ownershipId)
        {
            return _ownershipService.GetOwnershipFightRecord(ownershipId);
        }

        [HttpPost]
        public ActionResult PutForSale(int ownershipId, decimal priceRequested)
        {
            var ownership = _repository.Get<Ownership>(ownershipId);
            if (ownership.Username == HttpContext.User.Identity.Name || HttpContext.User.IsInRole("admin"))
            {
                _ownershipService.PutOwnershipForSale(ownershipId, priceRequested);
            }            
            return RedirectToAction("Index","UnderContract");
        }

        [HttpPost]
        public ActionResult RemoveFromSale(int ownershipId)
        {
            var ownership = _repository.Get<Ownership>(ownershipId);
            if (ownership.Username == HttpContext.User.Identity.Name || HttpContext.User.IsInRole("admin"))
            {
                _ownershipService.PutOwnershipForSale(ownershipId, 0);
            }            
            return RedirectToAction("Index", "UnderContract");
        }
    }
}