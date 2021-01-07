using Medlatec.Infrastructure.Oracle;
using Medlatec.Infrastructure.SeedWorks;
using Medlatec.Infrastructure.UowInterceptor.Abstracts;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medlatec.Core.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly IInterceptorManager _interceptors;

        public UnitOfWork(IDbContext dbContext, ILogger<UnitOfWork> logger, IInterceptorManager interceptorManager)
        {
            _dbContext = dbContext;
            _logger = logger;
            _interceptors = interceptorManager;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await _interceptors.BeforeCommit();
                var changeSet = _dbContext.ChangeTracker.Entries().ToList();
                await _interceptors.Execute(changeSet);
                var result = await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                await _interceptors.AfterCommit();
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unit of work commit failed");
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
