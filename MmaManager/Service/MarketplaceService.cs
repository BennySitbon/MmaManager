using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MmaManager.DAL;
using MmaManager.Models;
using MmaManager.Models.Enums;

namespace MmaManager.Service
{
    public class MarketplaceService
    {
        private TransactionService _transactionService;
        private OwnershipService _ownershipService;

        public MarketplaceService(IRepository repository)
        {
            _transactionService = new TransactionService(repository);
            _ownershipService = new OwnershipService(repository);
        }

        public List<Ownership> GetAllOnSaleOwnershipsList()
        {
            return _ownershipService.GetAllAsList().Where(i => i.PriceRequested > 0).ToList();
        }


        public void BuyFighter(int ownershipId)
        {
            var ownership = _ownershipService.Get(ownershipId);
            var fighterId = ownership.FighterID;
            var transactionToSave = new Transaction
            {
                FromUser = HttpContext.Current.User.Identity.Name,
                ToUser = "admin@MmaManager.com",
                Amount = ownership.PriceRequested,
                TimeStamp = DateTime.Now,
                TransactionType = TransactionType.Sell,
                FighterID = fighterId
            };
            _transactionService.Add(transactionToSave);
            var ownershipToSave = new Ownership { FighterID = fighterId, Username = HttpContext.Current.User.Identity.Name, TransactionID = transactionToSave.TransactionID };
           _ownershipService.Add(ownershipToSave);
            _ownershipService.Remove(ownership);
        }

        
    }
}