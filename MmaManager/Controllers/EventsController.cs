using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MmaManager.DAL;
using MmaManager.Service;

namespace MmaManager.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventsService _eventService;

        public EventsController()
        {
            _eventService = new EventsService(new Repository());
        }
        // GET: Events
        public ActionResult Index()
        {
            var events = _eventService.GetAllAsList();
            return View(events);
        }

        public ActionResult Details(int id)
        {
            var e = _eventService.GetLoaded(id);
            return View(e);
        }
    }
}