using Medlatec.Core.Domain.AggregateModels.TenantAggregate;
using Medlatec.Infrastructure.Constants;
using Medlatec.Infrastructure.InitializationStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medlatec.Core.Infrastructure.SeedData
{
    public class SeedTenantInitialization : IInitializationStage
    {
        private readonly CoreDbContext _context;

        public SeedTenantInitialization(CoreDbContext context)
        {
            _context = context;
        }

        public int Order => 5;
        public async Task ExecuteAsync()
        {
            if (!_context.Set<Tenant>().Any())
            {
                var tenantId = Guid.Parse(AuthRole.SuperAdminId);
                var tenant = new Tenant(tenantId, "Medlatec", "quynv@medlatec.vn", "0369574322", "Nghĩa Dũng", true, "", "");
                _context.Add(tenant);
                // set tenant Pages
                if (!_context.Set<TenantPage>().Where(x => x.TenantId == tenantId).Any())
                {
                    var pages = _context.Set<Page>().ToList();
                    foreach(var page in pages)
                    {
                        _context.Add(new TenantPage(tenantId, page.Id, false));
                    }
                }
                _context.SaveChanges();
            }
        }
    }
}
