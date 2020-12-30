using System.Threading.Tasks;

namespace Medlatec.Infrastructure.UowInterceptor.Abstracts
{
    public interface IOnAfterCommit
    {
        Task OnAfterCommit();
    }
}
