using System;
using System.Linq;

namespace FubuPersistence.Storage
{
    public interface IEntityStorage<T> where T : class, IEntity
    {
        T Find(Guid id);
        void Update(T entity);
        void Remove(T entity);

        IQueryable<T> All();
        void DeleteAll();
    }
}