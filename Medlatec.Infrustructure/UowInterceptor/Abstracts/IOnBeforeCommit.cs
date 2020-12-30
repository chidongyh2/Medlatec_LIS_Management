using System.Threading.Tasks;

namespace Medlatec.Infrastructure.UowInterceptor.Abstracts
{
    public interface IOnBeforeCommit
    {
        Task OnBeforeCommit();
    }
}
