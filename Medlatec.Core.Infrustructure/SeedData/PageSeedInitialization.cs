﻿using System.Linq;
using System.Threading.Tasks;
using Medlatec.Core.Domain.AggregateModels.TenantAggregate;
using Medlatec.Core.Infrastructure.Repository;
using Medlatec.Infrastructure.InitializationStage;

namespace Medlatec.Core.Infrastructure.SeedData
{
    public class PageSeedInitialization : IInitializationStage
    {
        public int Order => 2;
        private readonly CoreDbContext _context;
        public PageSeedInitialization(CoreDbContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync()
        {
            if (!_context.Set<Page>().Any())
            {
                #region Configs.
                _context.Set<Page>().Add(new Page(1, "Cấu hình", "Cấu hình", "fa fa-cogs", 0, null, "javascript://", true, true));
                _context.Set<Page>().Add(new Page(2, "Trang", "Cấu hình trang", "fa fa-file-text-o", 1, 1, "/config/page", true, true));
                _context.Set<Page>().Add(new Page(3, "Quyền truy cập", "Cấu hình quyền truy cập.", "fa fa-user-secret", 2, 1, "/config/roles", true, true));
                _context.Set<Page>().Add(new Page(4, "Khách hàng", "Cấu hình khách hàng", "fa fa-users", 3, 1, "/config/tenants", true, true));
                #endregion
                _context.SaveChanges();
            }
        }
    }
}
