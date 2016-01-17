using System.Collections.Generic;
using MmaManager.DAL;

namespace MmaManager.Service
{
    public abstract class EntityServiceBase<T>
    {
        protected IRepository _repository;

        protected EntityServiceBase(IRepository repository)
        {
            _repository = repository;
        }

        public abstract List<T> GetAllAsList();
        public abstract T Get(int id);
        public abstract T GetLoaded(int id);
        public virtual void Add<T>(T entity) where T:class
        {
            _repository.AddToSet(entity);
        }

        public virtual void Remove<T>(T entity) where T : class
        {
            _repository.RemoveFromSet(entity);
        }
    }
}