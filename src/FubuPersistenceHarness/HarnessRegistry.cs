using FubuMVC.Core;
using FubuPersistence.RavenDb;

namespace FubuPersistenceHarness
{
    public class HarnessRegistry : FubuRegistry
    {
        public HarnessRegistry()
        {
            var dbSettings = new RavenDbSettings
            {
                RunInMemory = true,
                UseEmbeddedHttpServer = true
            };
            Services(x =>
            {
                x.AddService(dbSettings);
                x.AddService<IDocumentStoreConfigurationAction, SimpleEmbeddedRavenDbPortConfigurationAction>();
            });
        }
    }
}