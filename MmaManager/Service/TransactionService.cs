using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MmaManager.DAL;
using MmaManager.Models;

namespace MmaManager.Service
{
    public class TransactionService
    {
        private readonly IRepository _repository;

        public TransactionService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Transaction> GetTransactionList()
        {
            return GetAllTransactionsQuery().ToList();
        }

        public Transaction GetTransaction(int transactionId)
        {
            return GetAllTransactionsQuery().SingleOrDefault(i => i.TransactionID == transactionId);
        }
        private IQueryable<Transaction> GetAllTransactionsQuery()
        {
            return _repository.GetAll<Transaction>();
        }
    }
}