using System.Threading.Tasks;

namespace Medlatec.Infrastructure.UowInterceptor.Abstracts
{
    public interface IOnUpdate<T> where T : class
    {
        Task OnUpdate(T entity);
    }
}
