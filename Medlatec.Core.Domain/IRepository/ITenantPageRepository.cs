using Medlatec.Core.Domain.AggregateModels.TenantAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medlatec.Core.Domain.IRepository
{
    public interface ITenantPageRepository
    {
        Task<bool> CheckExists(Guid tenantId, int pageId);

        Task<int> Insert(TenantPage tenantPage);

        Task<int> Update(TenantPage tenantPage);

        Task<int> Inserts(List<TenantPage> tenantPages);

        Task<int> Delete(Guid tenantId, int pageId);

        Task<int> Deletes(List<TenantPage> tenantPages);

        Task<TenantPage> GetInfo(Guid tenantId, int pageId, bool isReadOnly = false);

        Task<List<TenantPage>> GetListByPageId(int pageId, bool isReadOnly = false);

        Task<List<TenantPage>> GetListByTenantId(Guid tenantId, bool isReadOnly = false);

        Task<int> DeleteByTenantId(Guid tenantId);

        Task<int> DeleteByPageId(int pageId);
    }
}
