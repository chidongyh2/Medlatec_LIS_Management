using System.Threading.Tasks;

namespace Medlatec.Infrastructure.UowInterceptor.Abstracts
{
    public interface IOnDelete<T> where T : class
    {
        Task OnDelete(T entity);
    }
}
