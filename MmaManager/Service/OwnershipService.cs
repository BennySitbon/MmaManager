using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MmaManager.DAL;
using MmaManager.Models;

namespace MmaManager.Service
{
    public class OwnershipService
    {
        private readonly IRepository _repository;

        public OwnershipService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Ownership> GetOwnershipList(string username = null)
        {
            return GetAllOwnershipsQuery(username).ToList();
        }
        private IQueryable<Ownership> GetAllOwnershipsQuery(string username = null)
        {
            var query =  _repository.GetAll<Ownership>();
            if (username != null)
            {
                query = query.Where(i => i.Username == username);
            }
            return query;
        }

        public Ownership GetOwnership(int ownershipId)
        {
            return GetAllOwnershipsQuery().Single(i => i.OwnershipID == ownershipId);
        }
        public decimal GetNetIncome(int ownershipId)
        {
            var ownership = GetOwnership(ownershipId);
            var incoming = from trans in _repository.GetAll<Transaction>()
                           where trans.ToUser == ownership.Username &&
                           ((trans.FightListing.BlueFighterFighterID == ownership.FighterID
                           && trans.FightListing.FightResult == FightResult.BlueWin) ||
                           (trans.FightListing.RedFighterFighterID == ownership.FighterID &&
                           trans.FightListing.FightResult == FightResult.RedWin)) &&
                           trans.TimeStamp > ownership.Transaction.TimeStamp
                           select trans;

            decimal total = 0;
            foreach (var t in incoming)
            {
                total += t.Amount;
            }
            var outgoing = from trans in _repository.GetAll<Transaction>()
                           where trans.FromUser == ownership.Username &&
                           trans.FighterID == ownership.FighterID
                           select trans;
            foreach (var t in outgoing)
            {
                total -= t.Amount;
            }
            return total;
        }

        public string GetOwnershipRecord(int ownershipID)
        {
            var wins = 0;
            var loses = 0;
            var draws = 0;
            var NC = 0;
            var ownership = GetOwnership(ownershipID);
            var query = from listing in _repository.GetAll<FightListing>()
                        where (listing.BlueFighterFighterID == ownership.FighterID ||
                            listing.RedFighterFighterID == ownership.FighterID) &&
                            listing.Event.Date > ownership.Transaction.TimeStamp
                        select listing;
            foreach (var f in query)
            {
                switch (f.FightResult)
                {
                    case FightResult.Draw:
                        draws++;
                        break;
                    case FightResult.NC:
                        NC++;
                        break;
                    default:
                        if (f.BlueFighterFighterID == ownership.FighterID && f.FightResult == FightResult.BlueWin)
                        {
                            wins++;
                        }
                        else if (f.RedFighterFighterID == ownership.FighterID && f.FightResult == FightResult.RedWin)
                        {
                            wins++;
                        }
                        else loses++;
                        break;
                }
            }
            var result = wins + "-" + loses;
            if (draws > 0) result = result + "-" + draws;
            if (NC > 0) result = result + " " + NC + " NC";
            return result;
        }
    }
}