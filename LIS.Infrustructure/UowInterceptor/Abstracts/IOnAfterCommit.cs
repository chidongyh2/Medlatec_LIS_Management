using System.Threading.Tasks;

namespace LIS.Infrastructure.UowInterceptor.Abstracts
{
    public interface IOnAfterCommit
    {
        Task OnAfterCommit();
    }
}
