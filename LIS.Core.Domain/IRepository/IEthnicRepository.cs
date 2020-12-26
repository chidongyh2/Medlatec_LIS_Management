using System.Collections.Generic;
using System.Threading.Tasks;
using LIS.Infrastructure.ViewModel;
using LIS.Infrastructure.Domain.SystemAggregate;

namespace LIS.Core.Domain.IRepository
{
    public interface IEthnicRepository
    {
        Task<Ethnic> GetInfo(int id, bool isReadonly);
        Task<List<EthnicSearchViewModel>> GetAll(string languageId);
    }
}
