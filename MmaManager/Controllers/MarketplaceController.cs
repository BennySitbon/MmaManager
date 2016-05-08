using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Models;
using MmaManager.ViewModels;
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
            List<Ownership> onSaleOwnerships;
            if (string.IsNullOrEmpty(searchString))
            {
                onSaleOwnerships = _marketplaceService.GetAllOnSaleOwnershipsList();
            }
            else
            {
                onSaleOwnerships = _marketplaceService.GetAllOnSaleOwnershipsList()
                        .Where(i => i.Fighter.FullName.ToLower().Contains(searchString.ToLower()))
                        .ToList();
            }
            return
                View(
                    onSaleOwnerships.Select(
                        ownership =>
                            new OnSaleOwnershipViewModel
                            {
                                Ownership = ownership,
                                CanBuy = _marketplaceService.CanBuy(ownership)
                            }));
        }

        public ActionResult BuyFighter(int ownershipId)
        {
            _marketplaceService.BuyFighter(ownershipId);
            return RedirectToAction("Index","UnderContract");
        }
    }
}
