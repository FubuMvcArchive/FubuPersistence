using FubuCore.Dates;
using FubuCore.Logging;
using FubuPersistence.InMemory;
using FubuPersistence.Reset;
using FubuPersistence.Storage;
using StructureMap.Configuration.DSL;

namespace FubuPersistence.StructureMap
{
    public class InMemoryPersistenceRegistry : Registry
    {
        public InMemoryPersistenceRegistry()
        {
            // This acts as "SetServiceIfNone"
            For<ISystemTime>().Add(SystemTime.Default());
            For<IInitialState>().Add<NulloInitialState>();

            For<ITransaction>().Use<InMemoryTransaction>();

            For<IEntityRepository>().Use<EntityRepository>();
            For<IStorageRegistry>().Use<StorageRegistry>();

            ForSingletonOf<IPersistor>().Use<InMemoryPersistor>();

            For<IPersistenceReset>().Use<InMemoryPersistenceReset>();

            For<ICompleteReset>().Use<CompleteReset>();

            For<ILogger>().Use<Logger>();

            
        }
    }
}