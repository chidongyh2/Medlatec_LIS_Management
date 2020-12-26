using System.Threading.Tasks;

namespace LIS.Infrastructure.Oracle
{
    public interface IRepositoryBase
    {
        Task<int> Update();
    }
}
