using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Domain.DAL
{
    public class Repository : IRepository
    {
        private readonly UfcContext _db;

        public Repository()
        {
            _db = new UfcContext();
        }

        public T Get<T>(int id, bool loaded = false) where T : class
        {
            var all = GetAllQuery<T>();
            var func = loaded ?
                GetLoadedStrategy.GetStrategy(all, _db)
                : GetEntityStrategy.GetStrategy(all);
            return func(id);
        }

        public List<T> GetAll<T>(Func<IQueryable<T>, IEnumerable<T>> func = null) where T : class
        {
            return func == null ?
            GetAllQuery<T>().ToList()
            : func(GetAllQuery<T>()).ToList();
        }

        private IQueryable<T> GetAllQuery<T>() where T : class
        {
            return _db.Set<T>();
        }

        public void Add<T>(T entity) where T : class
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            //TODO: handle cascade delete event->fightlisting, maybe more
            _db.Entry(entity).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Upsert<T>(T entity) where T : class
        {
            _db.Set<T>().AddOrUpdate(entity);
            _db.SaveChanges();
        }
    }
}