using System.Linq;
using Domain.DAL;
using Domain.Models;
using Domain.Models.Enums;

namespace Service.Entity
{
    public class FightListingService : IFightListingService
    {
        private readonly IRepository _repository;

        public FightListingService(IRepository repository)
        {
            _repository = repository;
        }

        public void PayOwners(FightListing fightListing)
        {
            
            var ownerships =
                _repository.GetAll<Ownership>(
                    o => o.Where(i =>
                                (i.FighterID == fightListing.BlueFighterFighterID ||
                                i.FighterID == fightListing.RedFighterFighterID) &&
                                !i.Username.Equals(MmaManagerAdmin.UserName)));
            //TODO: better if could be done in one transaction
            ownerships.ForEach(o => _repository.Add(new Transaction
            {
                FromUser = MmaManagerAdmin.UserName,
                ToUser = o.Username,
                FightListingID = fightListing.FightListingID,
                FighterID = o.FighterID,
                TransactionType = TransactionType.Winnings,
                Amount = GetWinningsAmount(o, fightListing)
            }));
        }

        private decimal GetWinningsAmount(Ownership ownership, FightListing fightListing)
        {
            decimal total = 0;
            if (ownership.FighterID == fightListing.WinnerId)
            {
                total += ownership.Fighter.Worth;
                if (fightListing.FightBonus == BonusType.POTN)
                {
                    total += ownership.Fighter.Worth;
                }
            }
            else
            {
                total += ownership.Fighter.Worth / 2;
            }
            if (fightListing.FightBonus == BonusType.FOTN)
            {
                total += ownership.Fighter.Worth;
            }
            return total;
        }
    }
}
