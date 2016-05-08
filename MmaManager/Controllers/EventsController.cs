using System.Web.Mvc;
using Domain.DAL;
using Domain.Models;

namespace MmaManager.Controllers
{
    [Authorize(Roles = "admin")]
    public class EventsController : Controller
    {
        private readonly IRepository _repository;
        public EventsController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View(_repository.GetAll<Event>());
        }

        public ActionResult Details(int id)
        {
            return View(_repository.Get<Event>(id,true));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Date")] Event eve)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(eve);
                return RedirectToAction("Index");
            }
            return View(eve);
        }
    }
}