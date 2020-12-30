using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medlatec.Infrastructure.Domain.AccountAggregate;
using Medlatec.Infrastructure.ModelMetas;
using Medlatec.Infrastructure.Models;

namespace Medlatec.Core.Domain.IRepository
{
    public interface IRolePageRepository
    {
        Task<ActionResultResponse> Insert(RolePage rolePage);
        Task<int> Inserts(List<RolePage> rolePages);

        Task<int> Delete(Guid roleId, int pageId);

        Task<int> UpdatePermission(Guid roleId, int pageId, int permissions);

        Task<bool> CheckExists(int pageId, Guid roleId);

        Task<List<RolePage>> GetsByPageId(int pageId, bool isReadOnly = false);

        Task<List<RolePage>> GetsByRoleId(Guid roleId, bool isReadOnly = false);
        Task<ActionResultResponse> DeleteByPageId(int pageId);

        Task<int> DeleteByRoleIdAndPageIds(Guid roleId, List<int> pageId);

        Task<int> UpdateRolePagePermission(Guid roleId, List<RolePagePermissionMeta> rolePagePermissions);

        Task<ActionResultResponse> DeleteByRoleId(Guid roleId);

        Task<int> ForceDeleteByRoleId(Guid roleId);

        Task<RolePage> GetInfo(Guid roleId, int pageId);
    }
}
