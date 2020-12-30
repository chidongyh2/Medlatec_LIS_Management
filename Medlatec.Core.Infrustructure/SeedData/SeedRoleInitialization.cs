using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medlatec.Infrastructure.InitializationStage;
using Medlatec.Infrastructure.Constants;
using Medlatec.Infrastructure.Domain.AccountAggregate;

namespace Medlatec.Core.Infrastructure.SeedData
{
    public class SeedRoleInitialization : IInitializationStage
    {
        private readonly CoreDbContext _context;

        public SeedRoleInitialization(CoreDbContext context)
        {
            _context = context;
        }

        public int Order => 3;
        public async Task ExecuteAsync()
        {
            if (!_context.Set<Role>().Any())
            {
                var allRoles = new List<Role>
            {
                new Role(AuthRole.SuperAdmin, AuthRole.SuperAdmin, AuthRole.SuperAdmin)
            };
                foreach (var item in allRoles)
                {
                    _context.Add(item);
                }
                _context.SaveChanges();
            }
        }
    }
}
