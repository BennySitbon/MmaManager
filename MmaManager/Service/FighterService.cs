using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MmaManager.DAL;
using MmaManager.Models;

namespace MmaManager.Service
{
    public class FighterService : EntityServiceBase<Fighter>
    {

        public FighterService(IRepository repository):base(repository)
        {
            _repository = repository;
        }
        public override List<Fighter> GetAllAsList()
        {
            return GetAllFightersQuery().ToList();
        }

        private IQueryable<Fighter> GetAllFightersQuery()
        {
            return _repository.GetAll<Fighter>();
        }

        public override Fighter Get(int id)
        {
            return GetAllFightersQuery().FirstOrDefault(i => i.FighterId == id);
        }

        public override Fighter GetLoaded(int id)
        {
            var fighter = Get(id);
            var fightListings =
                _repository.GetAll<FightListing>()
                    .Where(l => l.BlueFighterFighterID == id || l.RedFighterFighterID == id).ToList();
            fighter.FightListings = fightListings;
            return fighter;
        }
        
    }
}