using System;
using FubuCore.Binding;
using StructureMap;
using StructureMap.Pipeline;

namespace FubuPersistence.InMemory
{
    public class InMemoryTransaction : TransactionBase
    {
        private readonly IContainer _container;

        public InMemoryTransaction(IContainer container)
        {
            _container = container;
        }

        public override void Execute<T>(ServiceArguments arguments, Action<T> action) 
        {
            using (var nested = _container.GetNestedContainer())
            {
                var explicits = new ExplicitArguments();
                arguments.EachService(explicits.Set);

                action(nested.GetInstance<T>(explicits));
            }
        }

    }
}