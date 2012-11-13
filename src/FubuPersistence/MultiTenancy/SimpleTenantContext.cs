using System;

namespace FubuPersistence.MultiTenancy
{
    public class SimpleTenantContext : ITenantContext
    {
        public Guid CurrentTenant { get; set; }
    }
}