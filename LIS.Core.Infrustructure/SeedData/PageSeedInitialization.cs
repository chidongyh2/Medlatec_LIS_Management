using System.Linq;
using System.Threading.Tasks;
using LIS.Core.Domain.AggregateModels.TenantAggregate;
using LIS.Core.Infrastructure.Repository;
using LIS.Infrastructure.InitializationStage;

namespace LIS.Core.Infrastructure.SeedData
{
    public class PageSeedInitialization : IInitializationStage
    {
        public int Order => 1;
        private readonly PageRepository _pageRepository;
        private readonly CoreDbContext _context;
        public PageSeedInitialization(CoreDbContext context, PageRepository pageRepository)
        {
            _context = context;
            _pageRepository = pageRepository;
        }

        public async Task ExecuteAsync()
        {
            if (!_context.Set<Page>().Any())
            {
                #region Configs.
                await _pageRepository.Insert(new Page(1, "Cấu hình", "Cấu hình", "fa fa-cogs", string.Empty, 0, null, "javascript://", true));
                await _pageRepository.Insert(new Page(2, "Trang", "Cấu hình trang", "fa fa-file-text-o", string.Empty, 1, 1, "/config/page", true, PageType.Tab));
                await _pageRepository.Insert(new Page(3, "Quyền truy cập", "Cấu hình quyền truy cập.", "fa fa-user-secret", string.Empty, 2, 1, "/config/roles", true, PageType.Tab));
                await _pageRepository.Insert(new Page(4, "Khách hàng", "Cấu hình khách hàng", "fa fa-users", string.Empty, 3, 1, "/config/tenants", true, PageType.Tab));
                #endregion
                _context.SaveChanges();
            }
        }
    }
}
