using System;
using System.Diagnostics;
using Raven.Client;

namespace FubuPersistence.RavenDb
{
    public class SessionBoundary : ISessionBoundary
    {
        private readonly IDocumentStore _store;
        private Lazy<IDocumentSession> _session;

        public SessionBoundary(IDocumentStore store)
        {
            _store = store;

            reset();
        }

        public void Dispose()
        {
            WithOpenSession(s => s.Dispose());
        }

        public IDocumentSession Session()
        {
            return _session.Value;
        }

        public IDocumentSession Session<T>() where T : RavenDbSettings
        {
            throw new NotImplementedException();
        }

        public bool WithOpenSession(Action<IDocumentSession> action)
        {
            if (_session != null && _session.IsValueCreated)
            {
                action(_session.Value);
                return true;
            }

            return false;
        }

        public void SaveChanges()
        {
            WithOpenSession(s => s.SaveChanges());
        }

        public void Start()
        {
            reset();
        }

        public void MakeReadOnly()
        {
            // TODO -- figure out how to make the entire document store / session be readonly 
            // Nothing yet, dadgummit
        }

        private void reset()
        {
            WithOpenSession(s => s.Dispose());
            _session = new Lazy<IDocumentSession>(() =>
            {
                Debug.WriteLine("Opening a new DocumentSession");
                return _store.OpenSession();
            });
        }
    }
}