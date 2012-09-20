using System;
using FubuCore.Binding;

namespace FubuPersistence
{
    public interface ITransaction
    {
        void Execute<T>(ServiceArguments arguments, Action<T> action) where T : class;
        void WithRepository(Action<IEntityRepository> action);
    }
}