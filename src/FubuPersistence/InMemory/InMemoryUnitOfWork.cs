using System;

namespace FubuPersistence.InMemory
{
    public class InMemoryUnitOfWork : IUnitOfWork, IDisposable
    {
        public void Dispose()
        {
            // no-op;
        }

        public void StartTransaction()
        {
        }

        public void Commit()
        {
        }

        public void RejectChanges()
        {
        }
    }
}