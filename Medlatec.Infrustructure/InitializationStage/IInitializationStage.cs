using System.Threading.Tasks;

namespace Medlatec.Infrastructure.InitializationStage
{
    public interface IInitializationStage
    {
        int Order { get; }
        Task ExecuteAsync();
    }
}
