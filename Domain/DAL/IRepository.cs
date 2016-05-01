using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.DAL
{
    public interface IRepository
    {
        T Get<T>(int id,bool loaded = false) where T : class;
        List<T> GetAll<T>(Func<IQueryable<T>,IEnumerable<T>> filter = null) where T : class;
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
    }
}