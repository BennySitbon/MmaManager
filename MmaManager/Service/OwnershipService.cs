﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MmaManager.DAL;
using MmaManager.Models;

namespace MmaManager.Service
{
    public class OwnershipService //:EntityServiceBase<Ownership>
    {
        private readonly IRepository _repository;

        public OwnershipService(IRepository repository) //:base(repository)
        {
            _repository = repository;
        }

        public decimal GetNetIncome(int ownershipId)
        {
            var ownership = _repository.Get<Ownership>(ownershipId);

            var incoming = _repository.GetAll<Transaction>(
                t => t.Where( trans => trans.ToUser == ownership.Username &&
                    ((trans.FightListing.BlueFighterFighterID == ownership.FighterID
                           && trans.FightListing.FightResult == FightResult.BlueWin) ||
                           (trans.FightListing.RedFighterFighterID == ownership.FighterID &&
                           trans.FightListing.FightResult == FightResult.RedWin)) &&
                           trans.TimeStamp > ownership.Transaction.TimeStamp))
                           .ToList();
                
            decimal total = 0;
            foreach (var t in incoming)
            {
                total += t.Amount;
            }

            var outgoing = _repository.GetAll<Transaction>(
                t => t.Where(trans => trans.FromUser == ownership.Username &&
                           trans.FighterID == ownership.FighterID).ToList());
            foreach (var t in outgoing)
            {
                total -= t.Amount;
            }
            return total;
        }

        public string GetOwnershipRecord(int ownershipID)
        {
            /*var wins = 0;
            var loses = 0;
            var draws = 0;
            var NC = 0;
            var ownership = Get(ownershipID);
            var query = from listing in Repository.GetAllQuery<FightListing>()
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
            return result;*/
            return "wait";
        }

        public void SellOwnership(int ownershipId, decimal priceRequested)
        {
            var ownership = _repository.Get<Ownership>(ownershipId);
            ownership.PriceRequested = priceRequested;
            _repository.Update(ownership);
        }
    }
}