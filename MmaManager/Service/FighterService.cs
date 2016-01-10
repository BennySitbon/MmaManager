using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MmaManager.DAL;
using MmaManager.Models;

namespace MmaManager.Service
{
    public class FighterService 
    {
        private readonly IRepository _repository;

        public FighterService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Fighter> GetFightersList()
        {
            return GetAllFightersQuery().ToList();
        }
        private IQueryable<Fighter> GetAllFightersQuery()
        {
            return _repository.GetAll<Fighter>();
        }

        public Fighter GetFighter(int id)
        {
            return GetAllFightersQuery().FirstOrDefault(i => i.FighterId == id);
        }

        public Fighter GetFighterWithFightListings(int id)
        {

            var fighter = GetFighter(id);
            var fightListings =
                _repository.GetAll<FightListing>()
                    .Where(l => l.BlueFighterFighterID == id || l.RedFighterFighterID == id).ToList();
            fighter.FightListings = fightListings;
            return fighter;
        }

        public void CreateFighter(Fighter fighter)
        {
            _repository.AddToSet(fighter);
        }

        
    }
}