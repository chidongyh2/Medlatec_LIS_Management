using Medlatec.Core.Infrastructure;
using Medlatec.Infrastructure.InitializationStage;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Medlatec.Core.Infrustructure.Initializations
{
    public class MigrateDatabaseInitialization : IInitializationStage
    {
        private readonly CoreDbContext _dbContext;
        public MigrateDatabaseInitialization(
            CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Order => 1;

        public async Task ExecuteAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}
