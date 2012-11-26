using FubuPersistence.InMemory;
using Raven.Client;
using StructureMap.Configuration.DSL;

namespace FubuPersistence.RavenDb
{
    public class RavenDbRegistry : Registry
    {
        public RavenDbRegistry()
        {
            IncludeRegistry<PersistenceRegistry>();

            Scan(x =>
            {
                x.AssemblyContainingType<IDocumentStoreConfigurationAction>();
                x.AddAllTypesOf<IDocumentStoreConfigurationAction>();
            });

            
            ForSingletonOf<IDocumentStore>().Use(c => c.GetInstance<DocumentStoreBuilder>().Build());

            For<IDocumentSession>().Use(c => c.GetInstance<ISessionBoundary>().Session());

            For<ISessionBoundary>().Use<SessionBoundary>();

            For<IPersistor>().Use<RavenPersistor>();

            For<ITransaction>().Use<RavenTransaction>();

            For<IUnitOfWork>().Use<RavenUnitOfWork>();
        }
    }
}