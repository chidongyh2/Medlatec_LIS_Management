using Medlatec.Core.Domain.IRepository;
using Medlatec.Infrastructure.Domain.SystemAggregate;
using Medlatec.Infrastructure.Oracle;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Medlatec.Core.Infrastructure.Repository
{
    public class ReligionRepository :RepositoryBase, IReligionRepository
    {
        private readonly IRepository<Religion> _regilionRepository;

        public ReligionRepository(IDbContext context) : base(context)
        {
            _regilionRepository = context.GetRepository<Religion>();
        }

        public async Task<List<T>> GetForSelect<T>(Expression<Func<Religion, T>> projector)
        {
            Expression<Func<Religion, bool>> spec = x => x.Name != string.Empty;

            var sort = Context.Filters.Sort<Religion, string>(x => x.Name);
            return await _regilionRepository.GetsAsAsync(projector, spec, sort);
        }

        public async Task<Religion> GetInfo(Guid id, bool isReadonly)
        {
            return await _regilionRepository.GetAsync(isReadonly, c => c.Id == id);
        }
    }
}
