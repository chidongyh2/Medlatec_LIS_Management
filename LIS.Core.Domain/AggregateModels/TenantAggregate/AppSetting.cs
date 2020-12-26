using LIS.Infrastructure.SeedWorks;
using System;

namespace LIS.Core.Domain.AggregateModels.TenantAggregate
{
    public class AppSetting : ModifierTrackingEntity
    {
        public Guid TenantId { get; private set; }
        public string Key { get; private set; }
        public string GroupId { get; private set; }
        public string Value { get; private set; }
        public virtual Tenant Tenant { get; private set; }
    }
}
