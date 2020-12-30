using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medlatec.Infrastructure.UowInterceptor.Abstracts
{
    public interface IInterceptorManager
    {
        Task Execute(IEnumerable<EntityEntry> changeSet);
        Task BeforeCommit();
        Task AfterCommit();
    }
}
