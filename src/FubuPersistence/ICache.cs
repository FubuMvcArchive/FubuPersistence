using System;
using System.Collections.Generic;

namespace FubuPersistence
{
    public interface ICache
    {
        IEnumerable<T> FetchAll<T>(Func<IEnumerable<T>> finder) where T : class, IEntity;
        T Find<T>(Guid id, Func<IEnumerable<T>> finder) where T : class, IEntity;
        bool HasLoaded<T>();
        void Replace<T>(IEnumerable<T> entities) where T : class, IEntity;
        void Update<T>(T entity, Func<IEnumerable<T>> finder) where T : class, IEntity;


        void Remove<T>(T entity) where T : class, IEntity;
        void ClearAll<T>();
    }
}