using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MmaManager.DAL;

namespace MmaManager.Service
{
    public class UserStatisticsService
    {
        private TransactionService _transactionService;
        private OwnershipService _ownershipService;

        public UserStatisticsService()
        {
            _transactionService = new TransactionService(new Repository());
            _ownershipService = new OwnershipService(new Repository());
        }
        public decimal GetUserWorth(string username)
        {
            decimal worth = 0;
            var ownerships = _ownershipService.GetOwnershipListForUser(username);
            ownerships.ForEach(i => worth += i.Fighter.Worth);
            var transactions = _transactionService.GetTransactionsForUser(username).Where(t => t.FightListingID != null).ToList();
            transactions.ForEach(t => worth+=t.AmountForUser(username));
            return worth;
        }
    }
}