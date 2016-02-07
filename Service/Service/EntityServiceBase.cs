using System.Collections.Generic;
using Domain.DAL;

namespace Service.Service
{
    public abstract class EntityServiceBase<T> where T : class
    {
        protected IRepository Repository;

        protected EntityServiceBase(IRepository repository)
        {
            Repository = repository;
        }

        public abstract List<T> GetAllAsList();
        public abstract T Get(int id);
        public abstract T GetLoaded(int id);
        public virtual void Add(T entity)
        {
            Repository.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            Repository.Delete(entity);
        }

        public virtual void Update(T entity)
        {
            Repository.Update(entity);
        }
    }
}