using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.DAL;
using Domain.Models;
using Domain.Models.Enums;

namespace Service.Service
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
            //return _ownershipService.GetAllAsList().Where(i => i.PriceRequested > 0).ToList();
            return _repository.GetAll<Ownership>(o => o.Where(i => i.PriceRequested > 0).ToList());
        }


        public void BuyFighter(int ownershipId)
        {
            //var ownership = _ownershipService.Get(ownershipId);
            var ownership = _repository.Get<Ownership>(ownershipId);
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
            //_transactionService.Add(transactionToSave);
            _repository.Add(transactionToSave);
            var ownershipToSave = new Ownership { FighterID = fighterId, Username = HttpContext.Current.User.Identity.Name, TransactionID = transactionToSave.TransactionID };
           //_ownershipService.Add(ownershipToSave);
            //_ownershipService.Remove(ownership);
            _repository.Add(ownershipToSave);
            _repository.Delete(ownership);
        }

        
    }
}