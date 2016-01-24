using System.Linq;

namespace MmaManager.DAL
{
    public interface IRepository
    {
        IQueryable<T> GetAll<T>() where T : class;
        void AddToSet<T>(T entity) where T : class;
        void RemoveFromSet<T>(T entity) where T : class;
        void UpdateEntity<T>(T entity) where T : class;
    }
}