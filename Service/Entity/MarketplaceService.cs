using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.DAL;
using Domain.Models;
using Domain.Models.Enums;

namespace Service.Entity
{
    public class MarketplaceService : IMarketplaceService
    {

        private readonly IRepository _repository;
        public MarketplaceService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Ownership> GetAllOnSaleOwnershipsList()
        {
            return _repository.GetAll<Ownership>(o => o.Where(i => i.PriceRequested > 0).ToList());
        }


        public void BuyFighter(int ownershipId)
        {
            var ownership = _repository.Get<Ownership>(ownershipId);
            var fighterId = ownership.FighterID;
            var transactionToSave = new Transaction
            {
                FromUser = HttpContext.Current.User.Identity.Name,
                ToUser = ownership.Username,
                Amount = ownership.PriceRequested,
                TransactionType = TransactionType.Sell,
                FighterID = fighterId
            };
            //TODO: Should worry about concurrency here
            _repository.Add(transactionToSave);
            var ownershipToSave = new Ownership { FighterID = fighterId, Username = HttpContext.Current.User.Identity.Name, TransactionID = transactionToSave.TransactionID };
            _repository.Add(ownershipToSave);
            _repository.Delete(ownership);
        }

        
    }
}