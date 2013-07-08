using System;
using FubuPersistence.RavenDb.Multiple;
using Raven.Client;
using StructureMap.Configuration.DSL;

namespace FubuPersistence.RavenDb
{
    public interface IDocumentStoreConfigurationAction
    {
        void Configure(IDocumentStore documentStore);
    }

    public interface IDocumentStoreConfigurationAction<T> : IDocumentStoreConfigurationAction where T : RavenDbSettings
    {
        
    }

    public class LambdaDocumentStoreConfigurationAction : IDocumentStoreConfigurationAction
    {
        private readonly Action<IDocumentStore> _configuration;

        public static IDocumentStoreConfigurationAction For(Action<IDocumentStore> configuration)
        {
            return new LambdaDocumentStoreConfigurationAction(configuration);
        }

        private LambdaDocumentStoreConfigurationAction(Action<IDocumentStore> configuration)
        {
            _configuration = configuration;
        }

        public void Configure(IDocumentStore documentStore)
        {
            _configuration(documentStore);
        }
    }

    public static class StructureMapRegistryExtensions
    {
        public static void RavenDbConfiguration(this Registry registry, Action<IDocumentStore> configuration)
        {
            var action = LambdaDocumentStoreConfigurationAction.For(configuration);
            registry.For<IDocumentStoreConfigurationAction>()
                    .Add(action);
        }

        public static void ConnectToRavenDb<T>(this Registry registry, Action<IDocumentStore> configuration = null) where T : RavenDbSettings
        {
            registry.ForSingletonOf<IDocumentStore<T>>().Use(new DocumentStoreInstance<T>());

            if (configuration != null)
            {
                registry.RavenDbConfiguration(configuration);
            }
        }
    }
}