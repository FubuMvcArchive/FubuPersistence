using System;
using FubuCore;

namespace FubuPersistence.Storage
{
    [MarkedForTermination, Obsolete]
    public class GlobalEntityStoragePolicy : IEntityStoragePolicy
    {
        public bool Matches<T>() where T : class, IEntity
        {
            return true;
        }

        public IEntityStorage<T> BuildStorage<T>(IPersistor persistor) where T : class, IEntity
        {
            return new GlobalEntityStorage<T>(persistor);
        }
    }
}