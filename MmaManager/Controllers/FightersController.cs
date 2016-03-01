using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.DAL;
using Domain.Models;
using Microsoft.AspNet.Identity;
using Service.Entity;

namespace MmaManager.Controllers
{
    //[Authorize(Roles = "player")]
    //TODO: Rename this
    [Authorize]
    public class FightersController : Controller
    {
        private readonly IRepository _repository;
        private readonly IOwnershipService _ownershipService;

        public FightersController(IRepository repository,IOwnershipService ownershipService)
        {
            _repository = repository;
            _ownershipService = ownershipService;
        }

        public ActionResult Index()
        {            
            if (User.IsInRole("admin"))
            {
                return View(_repository.GetAll<Ownership>());
            }
            var userName = User.Identity.GetUserName();
            return View(_repository.GetAll<Ownership>(o => o.Where(i => i.Username == userName).ToList()));
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
            //var fighter = _fighterService.GetLoaded(id);            
            var fighter = _repository.Get<Fighter>(id, true);
            if (fighter == null)
            {
                return HttpNotFound();
            }

            return View(fighter);
        }

        //TODO: Rename this to "put fighter on sale"?
        public ActionResult SellFighter(int ownershipId, decimal priceRequested)
        {
            //TODO: Finalize the view
            _ownershipService.SellOwnership(ownershipId,priceRequested);
            return RedirectToAction("Index");
        }
    }
}
