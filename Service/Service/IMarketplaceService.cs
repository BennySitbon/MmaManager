using System.Collections.Generic;
using Domain.Models;

namespace Service.Service
{
    public interface IMarketplaceService
    {
        List<Ownership> GetAllOnSaleOwnershipsList();
        void BuyFighter(int ownershipId);
    }
}