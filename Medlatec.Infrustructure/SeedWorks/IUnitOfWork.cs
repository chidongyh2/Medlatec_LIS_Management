using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medlatec.Infrastructure.SeedWorks
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
