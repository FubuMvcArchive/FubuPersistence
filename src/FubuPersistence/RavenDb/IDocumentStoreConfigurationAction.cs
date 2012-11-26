using System.Collections.Generic;
using Raven.Client;
using Raven.Imports.Newtonsoft.Json;

namespace FubuPersistence.RavenDb
{
    // TODO -- want all of this to a FubuMVC.RavenDb Bottle
    public interface IDocumentStoreConfigurationAction
    {
        void Configure(IDocumentStore documentStore);
    }

    public class CustomizeRavenJsonSerializer : IDocumentStoreConfigurationAction
    {
        private readonly IEnumerable<JsonConverter> _converters;

        public CustomizeRavenJsonSerializer(IEnumerable<JsonConverter> converters)
        {
            _converters = converters;
        }

        public void Configure(IDocumentStore documentStore)
        {
            documentStore.Conventions.CustomizeJsonSerializer = s =>
            {
                s.TypeNameHandling = TypeNameHandling.All;
                s.Converters.AddRange(_converters);
            };
        }
    }
}