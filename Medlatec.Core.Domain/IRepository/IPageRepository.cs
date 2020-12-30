using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Medlatec.Core.Domain.AggregateModels.TenantAggregate;
using Medlatec.Infrastructure.ViewModel;

namespace Medlatec.Core.Domain.IRepository
{
    public interface IPageRepository
    {
        Task<bool> CheckExists(int id);
        Task<int> Insert(Page page);
        Task<int> Update(Page pageMeta);
        Task<int> UpdateIdPath(int id, string idPath);
        Task<int> UpdateIdPath(string oldIdPath, string newIdPath);
        Task<int> UpdateChildrenIdPath(string oldParentIdPath, string newParentIdPath);
        Task<int> UpdateChildCount(int id);
        Task<int> Delete(int id);
        Task<int> ForceDelete(int id);
        Task<int> UpdateActive(int id, bool isActive);
        Task<Page> GetInfo(int id, bool? isReadOnly = true);
    }
}
