using System.Collections.Generic;
using System.Threading.Tasks;
using Medlatec.Infrastructure.ViewModel;
using Medlatec.Infrastructure.Domain.SystemAggregate;

namespace Medlatec.Core.Domain.IRepository
{
    public interface IEthnicRepository
    {
        Task<Ethnic> GetInfo(int id, bool isReadonly);
        Task<List<EthnicSearchViewModel>> GetAll(string languageId);
    }
}
