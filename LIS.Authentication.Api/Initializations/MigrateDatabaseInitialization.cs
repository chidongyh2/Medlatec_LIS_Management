using LIS.Authentication.Infrastructure;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LIS.Infrastructure.InitializationStage;

namespace LIS.Authentication.Initializations
{
    public class MigrateDatabaseInitialization : IInitializationStage
    {
        private readonly PersistedGrantDbContext _persistedGrantDbContext;
        private readonly ConfigurationDbContext _configurationDbContext;
        private readonly IdentityDbContext _dbContext;
        public MigrateDatabaseInitialization(
            IdentityDbContext dbContext,
            PersistedGrantDbContext persistedGrantDbContext,
            ConfigurationDbContext configurationDbContext)
        {
            _dbContext = dbContext;
            _persistedGrantDbContext = persistedGrantDbContext;
            _configurationDbContext = configurationDbContext;
        }

        public int Order => 2;

        public async Task ExecuteAsync()
        {
            await _dbContext.Database.MigrateAsync();
            await _configurationDbContext.Database.MigrateAsync();
            await _persistedGrantDbContext.Database.MigrateAsync();
        }
    }
}
