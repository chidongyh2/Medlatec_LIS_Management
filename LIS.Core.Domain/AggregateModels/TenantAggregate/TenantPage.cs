using LIS.Infrastructure.SeedWorks;
using System;

namespace LIS.Core.Domain.AggregateModels.TenantAggregate
{
    public class TenantPage : Entity
    {
        public Guid TenantId { get; private set; }
        public int PageId { get; private set; }
        public bool IsDelete { get; private set; }
        public virtual Page Page { get; private set; }
        public virtual Tenant Tenant { get; private set; }
        public TenantPage(Guid tenantId, int pageId, bool isDelete)
        {
            TenantId = tenantId;
            PageId = pageId;
            IsDelete = isDelete;
        }
    }
}
