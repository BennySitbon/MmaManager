using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MmaManager.DAL;
using MmaManager.Models;

namespace MmaManager.Service
{
    public class UserStatisticsService
    {
        //private TransactionService _transactionService;
        //private OwnershipService _ownershipService;
        private readonly IRepository _repository = new Repository();

        public UserStatisticsService()
        {
            //_transactionService = new TransactionService(new Repository());
            //_ownershipService = new OwnershipService(new Repository());
        }
        public decimal GetUserWorth(string username)
        {
            decimal worth = 0;
            //var ownerships = _ownershipService.GetOwnershipListForUser(username);
           // ownerships.ForEach(i => worth += i.Fighter.Worth);
            //var transactions = _transactionService.GetTransactionsForUser(username).Where(t => t.FightListingID != null).ToList();
            var transactions = _repository.GetAll<Transaction>(t => t.Where(transaction =>
                transaction.FromUser == username || transaction.ToUser == username).ToList());
            transactions.ForEach(t => worth+=t.AmountForUser(username));
            return worth;
        }
    }
}