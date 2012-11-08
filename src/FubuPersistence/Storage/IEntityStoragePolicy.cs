using System;
using FubuCore;

namespace FubuPersistence.Storage
{
    [MarkedForTermination, Obsolete]
    public interface IEntityStoragePolicy
    {
        bool Matches<T>() where T : class, IEntity;
        IEntityStorage<T> BuildStorage<T>(IPersistor persistor) where T : class, IEntity;
    }
}