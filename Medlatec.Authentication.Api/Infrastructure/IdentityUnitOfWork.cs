using Medlatec.Authentication.Infrastructure;
using Medlatec.Infrastructure.SeedWorks;
using Medlatec.Infrastructure.UowInterceptor.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medlatec.Authentication.Api.Infrastructure
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private readonly IdentityDbContext _dbContext;
        private readonly ILogger<IdentityUnitOfWork> _logger;
        private readonly IInterceptorManager _interceptors;

        public IdentityUnitOfWork(IdentityDbContext dbContext, ILogger<IdentityUnitOfWork> logger, IInterceptorManager interceptorManager)
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
                var changeSet = _dbContext.ChangeTracker.Entries()
                    .Where(t => t.State != EntityState.Unchanged && t.State != EntityState.Detached).ToList();
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
