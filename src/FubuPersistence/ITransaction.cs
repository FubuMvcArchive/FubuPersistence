using System;
using FubuCore.Binding;
using FubuPersistence.MultiTenancy;

namespace FubuPersistence
{
    public interface ITransaction
    {
        void Execute<T>(ServiceArguments arguments, Action<T> action) where T : class;

        void Execute<T>(Action<T> action) where T : class;
    }

    public static class TransactionExtensions
    {
        public static void WithRepository(this ITransaction transaction, Action<IEntityRepository> action)
        {
            transaction.Execute(action);
        }

        public static void WithRepository(this ITransaction transaction, Guid tenantId, Action<IEntityRepository> action)
        {
            var arguments = new ServiceArguments().With<ITenantContext>(new SimpleTenantContext {CurrentTenant = tenantId});
            transaction.Execute(arguments, action);
        }
    }
}