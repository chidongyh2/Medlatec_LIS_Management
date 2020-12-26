using System.Threading.Tasks;

namespace LIS.Infrastructure.UowInterceptor.Abstracts
{
    public interface IOnDelete<T> where T : class
    {
        Task OnDelete(T entity);
    }
}
