using System.Linq;
using Domain.DAL;
using Domain.Models;

namespace Service.Service
{
    public class OwnershipService : IOwnershipService
    {
        private readonly IRepository _repository;

        public OwnershipService(IRepository repository) 
        {
            _repository = repository;
        }

        public decimal GetNetIncome(int ownershipId)
        {
            var ownership = _repository.Get<Ownership>(ownershipId);

            var incoming = Enumerable.ToList(_repository.GetAll<Transaction>(
                               t => t.Where( trans => trans.ToUser == ownership.Username &&
                                                      ((trans.FightListing.BlueFighterFighterID == ownership.FighterID
                                                        && trans.FightListing.FightResult == FightResult.BlueWin) ||
                                                       (trans.FightListing.RedFighterFighterID == ownership.FighterID &&
                                                        trans.FightListing.FightResult == FightResult.RedWin)) &&
                                                      trans.TimeStamp > ownership.Transaction.TimeStamp)));
                
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
            var wins = 0;
            var loses = 0;
            var draws = 0;
            var NC = 0;
            var ownership = _repository.Get<Ownership>(ownershipID);
            var listings = _repository.GetAll<FightListing>(f => f.Where(
                listing => (listing.BlueFighterFighterID == ownership.FighterID ||
                            listing.RedFighterFighterID == ownership.FighterID) &&
                           listing.Event.Date > ownership.Transaction.TimeStamp));
            foreach (var f in listings)
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

        public void SellOwnership(int ownershipId, decimal priceRequested)
        {
            var ownership = _repository.Get<Ownership>(ownershipId);
            ownership.PriceRequested = priceRequested;
            _repository.Update(ownership);
        }
    }
}