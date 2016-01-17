using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MmaManager.Models;

namespace MmaManager.DAL
{
    public class Repository : IRepository
    {
        private readonly UfcContext _db;

        public Repository()
        {
            _db = new UfcContext();
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _db.Set<T>();
        }
        public void AddToSet<T>(T entity) where T : class
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
        }

        public void RemoveFromSet<T>(T entity) where T : class
        {
            _db.Set<T>().Remove(entity);
            _db.SaveChanges();
        }

    }
}