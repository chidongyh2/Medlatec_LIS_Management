using Medlatec.Infrastructure.Domain.SystemAggregate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Medlatec.Core.Domain.IRepository
{
    public interface IReligionRepository
    {
        Task<Religion> GetInfo(Guid id, bool isReadonly);
        Task<List<T>> GetForSelect<T>(Expression<Func<Religion, T>> projector);
    }
}
