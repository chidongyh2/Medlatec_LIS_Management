using System;
using System.Threading.Tasks;
using Medlatec.Core.Domain.AggregateModels.TenantAggregate;
using Medlatec.Core.Domain.IRepository;
using Medlatec.Infrastructure.Oracle;

namespace Medlatec.Core.Infrastructure.Repository
{
    public class TenantRepository : RepositoryBase, ITenantRepository
    {
        private readonly IRepository<Tenant> _tenantRepository;
        public TenantRepository(IDbContext context) : base(context)
        {
            _tenantRepository = Context.GetRepository<Tenant>();
        }

        public async Task<int> Insert(Tenant tenant)
        {
            _tenantRepository.Create(tenant);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> Update(Tenant tenant)
        {         
            return await Context.SaveChangesAsync();
        }

        public async Task<int> UpdateActiveStatus(Guid id, bool isActive)
        {
            var info = await GetInfo(id);
            if (info == null)
                return -1;

            info.SetActive(isActive);
            return await Context.SaveChangesAsync();
        }

        public async Task<Tenant> GetInfo(Guid id)
        {
            return await _tenantRepository.GetAsync(false, x => x.Id == id);
        }

        public async Task<bool> CheckExists(Guid id, string phoneNumber, string email)
        {
            return await _tenantRepository.ExistAsync(x =>
                x.Id != id && (x.PhoneNumber == phoneNumber || x.Email == email));
        }

        public async Task<int> Delete(Guid id)
        {
            var tenantInfo = await GetInfo(id);
            if (tenantInfo == null)
                return -1;

             _tenantRepository.Delete(tenantInfo);
            return await Context.SaveChangesAsync();
        }
    }
}
