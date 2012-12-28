using System;
using FubuCore;
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

        public bool IsEmpty()
        {
            return !RunInMemory && DataDirectory.IsEmpty() && Url.IsEmpty();
        }

        public IDocumentStore Create()
        {
            if (Url.IsNotEmpty())
            {
                return new DocumentStore
                {
                    Url = Url
                };
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