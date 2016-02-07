using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MmaManager.DAL;
using MmaManager.Models;
using MmaManager.Service;

namespace MmaManager.Controllers
{
    public class EventsController : Controller
    {
        //private readonly EventsService _eventService;
        private readonly IRepository _repository;
        public EventsController(IRepository repository)
        {
            _repository = repository;
            // _eventService = new EventsService(new Repository());
        }

        // GET: Events
        public ActionResult Index()
        {
            //var events = _eventService.GetAllAsList();            
            return View(_repository.GetAll<Event>());
        }

        public ActionResult Details(int id)
        {
            //var e = _eventService.GetLoaded(id);
            return View(_repository.Get<Event>(id,true));
        }
    }
}