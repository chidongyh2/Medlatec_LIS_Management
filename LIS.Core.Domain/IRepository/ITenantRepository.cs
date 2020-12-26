using LIS.Core.Domain.AggregateModels.TenantAggregate;
using System;
using System.Threading.Tasks;

namespace LIS.Core.Domain.IRepository
{
    public interface ITenantRepository
    {
        Task<int> Insert(Tenant tenant);

        Task<int> Update(Tenant tenant);

        Task<int> UpdateActiveStatus(Guid id, bool isActive);

        Task<Tenant> GetInfo(Guid id);

        Task<bool> CheckExists(Guid id, string phoneNumber, string email);

        Task<int> Delete(Guid id);
    }
}
