using FubuCore.Dates;
using FubuPersistence.InMemory;
using FubuPersistence.Storage;
using StructureMap.Configuration.DSL;

namespace FubuPersistence.StructureMap
{
    public class InMemoryRegistry : Registry
    {
        public InMemoryRegistry()
        {
            For<IUnitOfWork>().Use<InMemoryUnitOfWork>();
            For<IReset>().Use<InMemoryReset>();

            For<IReadOnlySession>().Use<InMemoryReadOnlySession>();

            For<ITransaction>().Use<InMemoryTransaction>();

            For<IEntityRepository>().Use<EntityRepository>();
            For<IStorageRegistry>().Use<StorageRegistry>();

            For<ISystemTime>().Use(SystemTime.Default);

            ForSingletonOf<IPersistor>().Use<InMemoryPersistor>();
        }
    }
}