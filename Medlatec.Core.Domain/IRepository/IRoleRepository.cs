using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medlatec.Infrastructure.Domain.AccountAggregate;
using Microsoft.AspNetCore.Identity;

namespace Medlatec.Core.Domain.IRepository
{
    public interface IRoleRepository : IRoleStore<Role>
    {
        //Task<int> Insert(Role role);

        //Task<int> Update(Role role);

        //Task<int> Delete(string id);

        //Task<Role> GetInfo(string id, bool isReadOnly = false);        

        Task<bool> CheckRoleExistsByTenant(Guid tenantId, Guid roleId);

        Task<bool> CheckExists(Guid tenantId, Guid id, string name);

        Task<int> ForceDelete(Guid roleId);

        Task<Role> GetRoleByRoleName(string roleName, bool isReadOnly = false);   
    }
}
