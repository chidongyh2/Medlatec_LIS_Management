using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LIS.Core.Domain.AggregateModels.TenantAggregate;
using LIS.Core.Domain.IRepository;
using LIS.Infrastructure.Oracle;

namespace LIS.Core.Infrastructure.Repository
{
    public class TenantPageRepository : RepositoryBase, ITenantPageRepository
    {
        private readonly IRepository<TenantPage> _tenantPageRepository;
        public TenantPageRepository(IDbContext context) : base(context)
        {
            _tenantPageRepository = Context.GetRepository<TenantPage>();
        }

        public async Task<bool> CheckExists(Guid tenantId, int pageId)
        {
            return await _tenantPageRepository.ExistAsync(x => x.TenantId == tenantId && x.PageId == pageId);
        }

        public async Task<int> Delete(Guid tenantId, int pageId)
        {
            var tenantPageInfo = await GetInfo(tenantId, pageId);
            if (tenantPageInfo == null)
                return -1;

            _tenantPageRepository.Delete(tenantPageInfo);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> DeleteByTenantId(Guid tenantId)
        {
            var listTenantPage = await _tenantPageRepository.GetsAsync(false, x => x.TenantId == tenantId);
            if (listTenantPage == null || !listTenantPage.Any())
                return -1;

            _tenantPageRepository.Deletes(listTenantPage);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> DeleteByPageId(int pageId)
        {
            var listTenantPage = await _tenantPageRepository.GetsAsync(false, x => x.PageId == pageId);
            if (listTenantPage == null || !listTenantPage.Any())
                return -1;

            _tenantPageRepository.Deletes(listTenantPage);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> Deletes(List<TenantPage> tenantPages)
        {
            if (tenantPages == null || !tenantPages.Any())
                return -1;

            _tenantPageRepository.Deletes(tenantPages);
            return await Context.SaveChangesAsync();
        }

        public async Task<TenantPage> GetInfo(Guid tenantId, int pageId, bool isReadOnly = false)
        {
            return await _tenantPageRepository.GetAsync(isReadOnly, x => x.TenantId == tenantId && x.PageId == pageId);
        }

        public async Task<List<TenantPage>> GetListByPageId(int pageId, bool isReadOnly = false)
        {
            return await _tenantPageRepository.GetsAsync(isReadOnly, x => x.PageId == pageId);
        }

        public async Task<int> Insert(TenantPage tenantPage)
        {
            _tenantPageRepository.Create(tenantPage);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> Inserts(List<TenantPage> tenantPages)
        {
            if (tenantPages == null || !tenantPages.Any())
                return -1;

            _tenantPageRepository.Creates(tenantPages);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> Update(TenantPage tenantPage)
        {
            return await Context.SaveChangesAsync();
        }

        public async Task<List<TenantPage>> GetListByTenantId(Guid tenantId, bool isReadOnly = false)
        {
            return await _tenantPageRepository.GetsAsync(isReadOnly, x => x.TenantId == tenantId);
        }
    }
}
