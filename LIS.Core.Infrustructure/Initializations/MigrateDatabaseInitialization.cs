using LIS.Infrastructure.InitializationStage;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LIS.Core.Infrustructure.Initializations
{
    public class MigrateDatabaseInitialization : IInitializationStage
    {
        private readonly IdentityDbContext _dbContext;
        public MigrateDatabaseInitialization(
            IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Order => 2;

        public async Task ExecuteAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}
