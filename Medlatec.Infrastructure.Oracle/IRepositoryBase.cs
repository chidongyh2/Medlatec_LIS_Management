using System.Threading.Tasks;

namespace Medlatec.Infrastructure.Oracle
{
    public interface IRepositoryBase
    {
        Task<int> Update();
    }
}
