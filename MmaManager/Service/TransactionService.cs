﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MmaManager.DAL;
using MmaManager.Models;

namespace MmaManager.Service
{
    public class TransactionService :EntityServiceBase<Transaction>
    {

        public TransactionService(IRepository repository):base(repository)
        {
            _repository = repository;
        }

        public override List<Transaction> GetAllAsList()
        {
            return GetAllTransactionsQuery().ToList();
        }

        public override Transaction Get(int id)
        {
            return GetAllTransactionsQuery().SingleOrDefault(i => i.TransactionID == id);
        }

        public override Transaction GetLoaded(int id)
        {
            return GetAllTransactionsQuery()
                .Include("Fighter")
                .Include("FightListing")
                .SingleOrDefault(i => i.TransactionID == id);
        }

        private IQueryable<Transaction> GetAllTransactionsQuery()
        {
            return _repository.GetAll<Transaction>();
        }
    }
}