using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MmaManager.DAL;
using MmaManager.Models;

namespace MmaManager.Service
{
    public class FightListingService : EntityServiceBase<FightListing>
    {

        public FightListingService(IRepository repository): base(repository)
        {
            _repository = repository;
        }

        public override List<FightListing> GetAllAsList()
        {
            return GetAllFightListingsQuery().ToList();
        }

        private IQueryable<FightListing> GetAllFightListingsQuery()
        {
            return _repository.GetAll<FightListing>();
        }

        public override FightListing Get(int id)
        {
            return GetAllFightListingsQuery().Single(i => i.FightListingID == id);
        }

        public override FightListing GetLoaded(int id)
        {
            return GetAllFightListingsQuery()
                    .Include("RedFighter")
                    .Include("BlueFighter")
                    .Include("Event")
                    .Single(i => i.FightListingID == id);
        }
    }
}