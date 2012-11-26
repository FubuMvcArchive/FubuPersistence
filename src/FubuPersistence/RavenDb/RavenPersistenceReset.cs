using FubuPersistence.Reset;
using Raven.Client;
using StructureMap;

namespace FubuPersistence.RavenDb
{
    public class RavenPersistenceReset : IPersistenceReset
    {
        private readonly IContainer _container;

        public RavenPersistenceReset(IContainer container)
        {
            _container = container;
        }

        public void ClearPersistedState()
        {
            _container.Model.For<IDocumentStore>().Default.EjectObject();
            _container.Inject(new RavenDbSettings
            {
                RunInMemory = true
            });
        }

        public void CommitAllChanges()
        {
            // no-op for now
        }
    }
}