using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MmaManager.DAL;
using MmaManager.Models;

namespace MmaManager.Service
{
    public class EventsService : EntityServiceBase<Event>
    {

        public EventsService(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public override List<Event> GetAllAsList()
        {
            return GetAllEventsQuery().ToList();
        }

        private IQueryable<Event> GetAllEventsQuery()
        {
            return _repository.GetAll<Event>();
        }

        public override Event Get(int id)
        {
            return GetAllEventsQuery().Single(i => i.EventID == id);
        }

        public override Event GetLoaded(int id)
        {
            return GetAllEventsQuery().Include("FightCard").Single(i => i.EventID == id);
        }

    }
}