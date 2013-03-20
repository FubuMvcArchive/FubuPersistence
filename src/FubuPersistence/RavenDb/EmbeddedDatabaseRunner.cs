using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuPersistence.Reset;
using Raven.Client;
using Raven.Database.Server;

namespace FubuPersistence.RavenDb
{
    public class EmbeddedDatabaseRunner : IDisposable, IPersistenceReset
    {
        private IDocumentStore _store;

        public IDocumentStore Store
        {
            get { return _store; }
        }

        public void Dispose()
        {
            _store.Dispose();
        }

        public void Start()
        {
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);

            RavenDbSettings settings = RavenDbSettings.InMemory();
            settings.UseEmbeddedHttpServer = true;

            _store = settings.Create();
            _store.Initialize();
        }

        public RavenDbSettings SettingsToConnect()
        {
            return new RavenDbSettings{Url = "http://localhost:8080"};
        }

        public void ClearPersistedState()
        {
            if (_store != null)
            {
                _store.Dispose();
            }

            Start();
        }


        public void CommitAllChanges()
        {
            // do nothing
        }
    }
}