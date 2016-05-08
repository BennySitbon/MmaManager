using Domain.Models;

namespace Service.Entity
{
    public interface IFightListingService
    {
        void PayOwners(FightListing fightListing);
    }
}