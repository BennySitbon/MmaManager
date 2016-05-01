using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Entity;

namespace MmaManager.Controllers
{
    public class OwnershipController : Controller
    {
        private readonly IOwnershipService _ownershipService;

        public OwnershipController(IOwnershipService ownershipService)
        {
            _ownershipService = ownershipService;
        }

        public decimal GetNetIncome(int ownershipId)
        {
            return _ownershipService.GetNetIncome(ownershipId);
        }

        public string GetOwnershipRecord(int ownershipId)
        {
            return _ownershipService.GetOwnershipRecord(ownershipId);
        }

        public ActionResult PutForSale(int ownershipId, decimal priceRequested)
        {
            //TODO: Finalize the view
            _ownershipService.PutOwnershipForSale(ownershipId, priceRequested);
            return RedirectToAction("Index","UnderContract");
        }

        public ActionResult RemoveFromSale(int ownershipId)
        {
            _ownershipService.PutOwnershipForSale(ownershipId, 0);
            return RedirectToAction("Index", "UnderContract");
        }
    }
}