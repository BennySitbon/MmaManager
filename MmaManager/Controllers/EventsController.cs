using System.Web.Mvc;
using Domain.DAL;
using Domain.Models;

namespace MmaManager.Controllers
{
    public class EventsController : Controller
    {
        //TODO: Security for only admin
        private readonly IRepository _repository;
        public EventsController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Events
        public ActionResult Index()
        {
            return View(_repository.GetAll<Event>());
        }

        public ActionResult Details(int id)
        {
            return View(_repository.Get<Event>(id,true));
        }

        public ActionResult Create()
        {
            return View();
        }
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