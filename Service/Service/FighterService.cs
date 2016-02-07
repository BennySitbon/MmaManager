namespace Service.Service
{
    /*public class FighterService : EntityServiceBase<Fighter>
    {

        /*public FighterService(IRepository repository):base(repository)
        {
        }
        public override List<Fighter> GetAllAsList()
        {
            return GetAllFightersQuery().ToList();
        }

        private IQueryable<Fighter> GetAllFightersQuery()
        {
            return Repository.GetAllQuery<Fighter>();
        }

        public override Fighter Get(int id)
        {
            //var dp = new DataProvider(Repository);
            //return dp.Get<Fighter>(id);
            var a = Repository.Get<Fighter>(id, true);
            return a;

            //return GetAllFightersQuery().FirstOrDefault(i => i.FighterId == id);
        }

        public override Fighter GetLoaded(int id)
        {
            var fighter = Get(id);
            var fightListings =
                Repository.GetAllQuery<FightListing>()
                    .Where(l => l.BlueFighterFighterID == id || l.RedFighterFighterID == id).ToList();
            fighter.FightListings = fightListings;
            return fighter;
        }
        
    }*/
}