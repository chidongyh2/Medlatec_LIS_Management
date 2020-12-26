using System.Threading.Tasks;

namespace LIS.Infrastructure.InitializationStage
{
    public interface IInitializationStage
    {
        int Order { get; }
        Task ExecuteAsync();
    }
}
