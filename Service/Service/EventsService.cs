namespace Service.Service
{ /*
namespace MmaManager.Service
{
    public class EventsService : EntityServiceBase<Event>
    {

        public EventsService(IRepository repository) : base(repository)
        {
        }

        public override List<Event> GetAllAsList()
        {
            return GetAllEventsQuery().ToList();
        }

        private IQueryable<Event> GetAllEventsQuery()
        {
            return Repository.GetAllQuery<Event>();
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
}*/
}