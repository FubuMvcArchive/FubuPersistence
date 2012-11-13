using System;

namespace FubuPersistence.MultiTenancy
{
    public interface ITenantedEntity : IEntity
    {
        Guid TenantId { get; set; }
    }

    public interface ITenantContext
    {
        Guid CurrentTenant { get; }
    }

    public class NulloTenantContext : ITenantContext
    {
        public Guid CurrentTenant
        {
            get { return Guid.Empty; }
        }
    }
}