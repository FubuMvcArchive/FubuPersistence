using System;
using FubuCore.Binding;
using StructureMap;
using StructureMap.Pipeline;

namespace FubuPersistence.InMemory
{
    public class InMemoryTransaction : ITransaction
    {
        private readonly IContainer _container;

        public InMemoryTransaction(IContainer container)
        {
            _container = container;
        }

        public void Execute<T>(ServiceArguments arguments, Action<T> action) where T : class
        {
            using (var nested = _container.GetNestedContainer())
            {
                var explicits = new ExplicitArguments();
                arguments.EachService(explicits.Set);

                action(nested.GetInstance<T>(explicits));
            }
        }

        public void Execute<T>(Action<T> action) where T : class
        {
            using (var nested = _container.GetNestedContainer())
            {
                action(nested.GetInstance<T>());
            }
        }
    }
}