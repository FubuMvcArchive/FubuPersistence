using System;
using FubuCore;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;

namespace FubuPersistence.RavenDb
{
    /*
     *  TODO
     *  DataDirectory, if it's null, do it by finding the /ravendb folder parallel to the
     *  application
     * 

     */

    public class RavenDbSettings
    {
        public RavenDbSettings()
        {
            RunInMemory = false;
        }

        public bool RunInMemory { get; set; }
        public string DataDirectory { get; set; }
        public string Url { get; set; }

        public bool IsEmpty()
        {
            return !RunInMemory && DataDirectory.IsEmpty() && Url.IsEmpty();
        }

        public IDocumentStore Create()
        {
            if (Url.IsNotEmpty())
            {
                return new DocumentStore()
                {
                    Url = Url
                };
            }

            if (DataDirectory.IsNotEmpty())
            {
                return new EmbeddableDocumentStore
                {
                    DataDirectory = DataDirectory
                };
            }

            if (RunInMemory)
            {
                return new EmbeddableDocumentStore
                {
                    RunInMemory = true
                };
            }

            throw new ArgumentOutOfRangeException("settings");
        }
    }
}