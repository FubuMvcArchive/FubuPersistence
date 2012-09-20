using System.Collections.Generic;
using System.Linq;

namespace FubuPersistence.Storage
{
    public class StorageRegistry : IStorageRegistry
    {
        private readonly IPersistor _persistor;
        private readonly IEnumerable<IEntityStoragePolicy> _policies;

        public StorageRegistry(IPersistor persistor, IEnumerable<IEntityStoragePolicy> policies)
        {
            _persistor = persistor;
            _policies = policies;
        }

        public IEntityStorage<T> StorageFor<T>() where T : class, IEntity
        {
            var policy = _policies.LastOrDefault(x => x.Matches<T>()) ?? new GlobalEntityStoragePolicy();
            return policy.BuildStorage<T>(_persistor);
        }
    }
}