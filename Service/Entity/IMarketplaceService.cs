using System.Collections.Generic;
using Domain.Models;

namespace Service.Entity
{
    public interface IMarketplaceService
    {
        List<Ownership> GetAllOnSaleOwnershipsList();
        void BuyFighter(int ownershipId);
        bool CanBuy(Ownership ownership);
    }
}