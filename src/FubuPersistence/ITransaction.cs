using System;

namespace FubuPersistence
{
    public interface ITransaction
    {
        void Execute<T>(Action<T> action) where T : class;
        void WithRepository(Action<IEntityRepository> action);
    }
}