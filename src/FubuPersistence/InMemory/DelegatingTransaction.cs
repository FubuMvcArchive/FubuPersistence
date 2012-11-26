using System;
using FubuCore.Binding;
using StructureMap;

namespace FubuPersistence.InMemory
{
    public class DelegatingTransaction : ITransaction
    {
        private readonly IContainer _container;

        public DelegatingTransaction(IContainer container)
        {
            _container = container;
        }

        public void Execute<T>(ServiceArguments arguments, Action<T> action) where T : class
        {
            _container.Apply(arguments);
            Execute(action);
        }

        public void Execute<T>(Action<T> action) where T : class
        {
            action(_container.GetInstance<T>());
        }
    }
}