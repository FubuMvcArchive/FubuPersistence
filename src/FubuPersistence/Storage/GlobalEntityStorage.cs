using System;
using System.Linq;

namespace FubuPersistence.Storage
{
    public class GlobalEntityStorage<T> : IEntityStorage<T> where T : class, IEntity
    {
        private readonly IPersistor _persistor;

        public GlobalEntityStorage(IPersistor persistor)
        {
            _persistor = persistor;
        }

        public T Find(Guid id)
        {
            return _persistor.Find<T>(id);
        }

        public void Update(T entity)
        {
            _persistor.Persist(entity);
        }

        public void Remove(T entity)
        {
            _persistor.Remove(entity);
        }

        public IQueryable<T> All()
        {
            return _persistor.LoadAll<T>();
        }

        public void DeleteAll()
        {
            _persistor.DeleteAll<T>();
        }
    }
}