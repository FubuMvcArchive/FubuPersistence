using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuPersistence.RavenDb;
using StructureMap.Configuration.DSL;

namespace FubuMVC.RavenDb
{
    public class RavenDbFubuRegistryExtension : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Policies.Add<TransactionalBehaviorPolicy>();

            registry.Services(x => { x.AddService<Registry, RavenDbRegistry>(); });
        }
    }

    [ConfigurationType(ConfigurationType.InjectNodes)]
    public class TransactionalBehaviorPolicy : Policy
    {
        public TransactionalBehaviorPolicy()
        {
            Where.RespondsToHttpMethod("POST", "PUT", "DELETE");
            Wrap.WithBehavior<TransactionalBehavior>();
        }
    }
}