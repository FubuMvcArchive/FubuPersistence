using System;
using Raven.Client;

namespace FubuPersistence.RavenDb
{
    public interface ISessionBoundary : IDisposable
    {
        IDocumentSession Session();
        IDocumentSession Session<T>() where T : RavenDbSettings;
        bool WithOpenSession(Action<IDocumentSession> action);
        void SaveChanges();
        void Start();
        void MakeReadOnly();
    }
}