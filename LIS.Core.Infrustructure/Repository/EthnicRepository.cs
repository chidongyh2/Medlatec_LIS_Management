using LIS.Core.Domain.IRepository;
using LIS.Infrastructure.Domain.SystemAggregate;
using LIS.Infrastructure.Oracle;
using LIS.Infrastructure.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LIS.Core.Infrastructure.Repository
{
    public class EthnicRepository : RepositoryBase, IEthnicRepository
    {
        private readonly IRepository<Ethnic> _ethnicRepository;
        public EthnicRepository(IDbContext context) : base(context)
        {
            _ethnicRepository = Context.GetRepository<Ethnic>();
        }

        public async Task<Ethnic> GetInfo(int id, bool isReadonly)
        {
            return await _ethnicRepository.GetAsync(isReadonly, c => c.Id == id);
        }

        public Task<List<EthnicSearchViewModel>> GetAll(string languageId)
        {
            var query = Context.Set<Ethnic>().AsNoTracking()
                .Select(c => new EthnicSearchViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                });

            return query.ToListAsync();
        }
    }
}
