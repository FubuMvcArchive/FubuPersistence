using System;
using FubuCore;
using FubuCore.Binding;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;

namespace FubuPersistence.RavenDb
{
    public class RavenDbSettings
    {
        public string DataDirectory { get; set; }
        public bool RunInMemory { get; set; }
        public string Url { get; set; }
        public bool UseEmbeddedHttpServer { get; set; }

        [ConnectionString]
        public string ConnectionString { get; set; }

        public bool IsEmpty()
        {
            return !RunInMemory && DataDirectory.IsEmpty() && Url.IsEmpty();
        }

        public IDocumentStore Create()
        {
            if (Url.IsNotEmpty())
            {
                var store = new DocumentStore
                {
                    Url = Url
                };

                if (ConnectionString.IsNotEmpty())
                {
                    store.ParseConnectionString(ConnectionString);
                }

                return store;
            }

            if (DataDirectory.IsNotEmpty())
            {
                return new EmbeddableDocumentStore
                {
                    DataDirectory = DataDirectory,
                    UseEmbeddedHttpServer = UseEmbeddedHttpServer
                };
            }

            if (RunInMemory)
            {
                return new EmbeddableDocumentStore
                {
                    RunInMemory = true,
                    UseEmbeddedHttpServer = UseEmbeddedHttpServer
                };
            }

            throw new ArgumentOutOfRangeException("settings");
        }
    }
}