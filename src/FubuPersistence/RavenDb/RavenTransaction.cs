using System;
using FubuCore.Binding;
using StructureMap;

namespace FubuPersistence.RavenDb
{
    public class RavenTransaction : ITransaction
    {
        private readonly IContainer _container;

        public RavenTransaction(IContainer container)
        {
            _container = container;
        }

        public void Execute<T>(ServiceArguments arguments, Action<T> action) where T : class
        {
            using (IContainer nested = _container.GetNestedContainer())
            {
                nested.Apply(arguments);

                var service = nested.GetInstance<T>();
                action(service);

                nested.GetInstance<ISessionBoundary>().SaveChanges();
            }
        }

        public void Execute<T>(Action<T> action) where T : class
        {
            Execute(new ServiceArguments(), action);
        }
    }
}