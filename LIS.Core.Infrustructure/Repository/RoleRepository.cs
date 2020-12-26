using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LIS.Core.Domain.IRepository;
using LIS.Core.Domain.Resources;
using LIS.Infrastructure.Domain.AccountAggregate;
using LIS.Infrastructure.Extensions;
using LIS.Infrastructure.Helpers;
using LIS.Infrastructure.IServices;
using LIS.Infrastructure.Oracle;
using LIS.Infrastructure.Resources;
using Microsoft.AspNetCore.Identity;

namespace LIS.Core.Infrastructure.Repository
{
    public class RoleRepository : RepositoryBase, IRoleRepository
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IResourceService<SharedResource> _sharedResourceService;
        private readonly IResourceService<CoreResource> _resourceService;
        public RoleRepository(IDbContext context, IResourceService<SharedResource> sharedResourceService,
            IResourceService<CoreResource> resourceService) : base(context)
        {
            _sharedResourceService = sharedResourceService;
            _resourceService = resourceService;
            _roleRepository = Context.GetRepository<Role>();
        }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            _roleRepository.Create(role);
            var result = await Context.SaveChangesAsync(cancellationToken);
            return result <= 0
                ? IdentityResult.Failed(new IdentityError
                {
                    Code = result.ToString(),
                    Description =
                        _sharedResourceService.GetString("Something went wrong. Please contact with administrator.")
                })
                : IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
        
            var result = await Context.SaveChangesAsync(cancellationToken);
            return result <= 0 ? IdentityResult.Failed(new IdentityError { Code = result.ToString(), Description = _sharedResourceService.GetString("Something went wrong. Please contact with administrator.") })
                : IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            var roleInfo = await FindByIdAsync(role.Id.ToString(), new CancellationToken());
            if (roleInfo == null)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "-1",
                    Description = _resourceService.GetString("Role does not exists.")
                });

            _roleRepository.Delete(roleInfo);
            var result = await Context.SaveChangesAsync(cancellationToken);
            return result > 0 ? IdentityResult.Success : IdentityResult.Failed(new IdentityError
            {
                Code = "-3",
                Description = _sharedResourceService.GetString("Something went wrong. Please contact with administrator.")
            });
        }

        public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            var roleInfo = await FindByNameAsync(role.NormalizedName, new CancellationToken());
            return roleInfo == null ? string.Empty : roleInfo.Id.ToString();
        }

        public async Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            var roleInfo = await FindByIdAsync(role.Id.ToString(), cancellationToken);
            return roleInfo == null ? string.Empty : roleInfo.Name;
        }

        public async Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            var roleInfo = await FindByIdAsync(role.Id.ToString(), cancellationToken);
            if (roleInfo != null)
            {
                roleInfo.Name = role.Name.Trim();
                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            var roleInfo = await FindByIdAsync(role.Id.ToString(), new CancellationToken());
            return roleInfo == null ? string.Empty : roleInfo.NormalizedName;
        }

        public async Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            var roleInfo = await FindByIdAsync(role.Id.ToString(), cancellationToken);
            if (roleInfo != null)
            {
                roleInfo.NormalizedName = roleInfo.Name.StripVietnameseChars().ToLower();
                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await _roleRepository.GetAsync(false, x => x.Id.ToString() == roleId);
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await _roleRepository.GetAsync(false, x => x.NormalizedName == normalizedRoleName);
        }

        public async Task<bool> CheckRoleExistsByTenant(Guid tenantId, Guid roleId)
        {
            return await _roleRepository.ExistAsync(x => x.Id == roleId && x.TenantId == tenantId);
        }

        public async Task<bool> CheckExists(Guid tenantId, Guid id, string name)
        {
            name = name.Trim();
            return await _roleRepository.ExistAsync(x => x.TenantId == tenantId && x.Id != id && x.Name == name);
        }

        public async Task<int> ForceDelete(Guid roleId)
        {
            var role = await _roleRepository.GetAsync(false, x => x.Id == roleId);
            if (role == null)
                return -1;

            _roleRepository.Delete(role);
            return await Context.SaveChangesAsync();
        }

        private async Task<bool> CheckNameExists(Guid tenantId, Guid id, string name)
        {
            return await _roleRepository.ExistAsync(x => x.Id != id && x.TenantId == tenantId && x.Name == name);
        }

        public async Task<Role> GetRoleByRoleName(string roleName, bool isReadOnly = false)
        {
            return await _roleRepository.GetAsync(false, x => x.Name == roleName);
        }
    }
}
