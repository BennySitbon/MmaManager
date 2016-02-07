using System.Collections.Generic;
using MmaManager.Models;

namespace MmaManager.Service
{
    public interface IMarketplaceService
    {
        List<Ownership> GetAllOnSaleOwnershipsList();
        void BuyFighter(int ownershipId);
    }
}