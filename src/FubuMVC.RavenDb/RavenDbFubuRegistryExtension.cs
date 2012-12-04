using FubuMVC.Core;
using FubuMVC.Core.Packaging;
using FubuMVC.Core.Registration;
using FubuPersistence.RavenDb;
using FubuCore;

namespace FubuMVC.RavenDb
{
    public class RavenDbFubuRegistryExtension : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Policies.Add<TransactionalBehaviorPolicy>();

            var defaultDataDirectory = FubuMvcPackageFacility.GetApplicationPath().ParentDirectory().AppendPath("data");
            var defaultSettings = new RavenDbSettings
            {
                DataDirectory = defaultDataDirectory
            };

            registry.Services(x => x.SetServiceIfNone(defaultSettings));
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