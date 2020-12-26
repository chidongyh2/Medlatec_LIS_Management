using System.Threading.Tasks;

namespace LIS.Infrastructure.UowInterceptor.Abstracts
{
    public interface IOnUpdate<T> where T : class
    {
        Task OnUpdate(T entity);
    }
}
