using System;
using Raven.Client;

namespace FubuPersistence.RavenDb
{
    public interface ISessionBoundary : IDisposable
    {
        IDocumentSession Session();
        bool WithOpenSession(Action<IDocumentSession> action);
        void SaveChanges();
        void Start();
        void MakeReadOnly();
    }
}