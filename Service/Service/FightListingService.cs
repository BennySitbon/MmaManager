namespace Service.Service
{ /*
namespace MmaManager.Service
{
    public class FightListingService : EntityServiceBase<FightListing>
    {

        public FightListingService(IRepository repository): base(repository)
        {
        }

        public override List<FightListing> GetAllAsList()
        {
            return GetAllFightListingsQuery().ToList();
        }

        private IQueryable<FightListing> GetAllFightListingsQuery()
        {
            return Repository.GetAllQuery<FightListing>();
        }

        public override FightListing Get(int id)
        {
            return GetAllFightListingsQuery().Single(i => i.FightListingID == id);
        }

        public override FightListing GetLoaded(int id)
        {
            return Repository.Get<FightListing>(id,true);
            /*return GetAllFightListingsQuery()
                    .Include("RedFighter")
                    .Include("BlueFighter")
                    .Include("Event")
                    .Single(i => i.FightListingID == id);
        }
        //TODO: adding a fight listing should automatically
        //TODO: pay to all users that own that fighter according to ranking
    }
}*/
}