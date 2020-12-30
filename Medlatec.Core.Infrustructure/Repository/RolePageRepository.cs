using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medlatec.Core.Domain.IRepository;
using Medlatec.Core.Domain.Resources;
using Medlatec.Infrastructure.Domain.AccountAggregate;
using Medlatec.Infrastructure.IServices;
using Medlatec.Infrastructure.ModelMetas;
using Medlatec.Infrastructure.Models;
using Medlatec.Infrastructure.Oracle;
using Medlatec.Infrastructure.Resources;

namespace Medlatec.Core.Infrastructure.Repository
{
    public class RolePageRepository : RepositoryBase, IRolePageRepository
    {
        private readonly IRepository<RolePage> _rolePageRepository;
        private readonly IResourceService<SharedResource> _sharedResourceService;
        private readonly IResourceService<CoreResource> _resourceService;

        public RolePageRepository(IDbContext context, IResourceService<SharedResource> sharedResourceService, IResourceService<CoreResource> resourceService) : base(context)
        {
            _sharedResourceService = sharedResourceService;
            _resourceService = resourceService;
            _rolePageRepository = Context.GetRepository<RolePage>();
        }

        public async Task<ActionResultResponse> Insert(RolePage rolePage)
        {
            var isExists = await CheckExists(rolePage.PageId, rolePage.RoleId);
            if (isExists)
                return new ActionResultResponse(-1, _resourceService.GetString("Role page already assigned."));

            _rolePageRepository.Create(rolePage);
            var result = await Context.SaveChangesAsync();
            return new ActionResultResponse(result, result > 0 ? _resourceService.GetString("Role page assign successful.")
                : _sharedResourceService.GetString("Something went wrong. Please contact with administrator."));
        }

        public async Task<int> Inserts(List<RolePage> rolePages)
        {
            _rolePageRepository.Creates(rolePages);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid roleId, int pageId)
        {
            var info = await _rolePageRepository.GetAsync(false, x => x.PageId == pageId && x.RoleId == roleId);
            if (info == null)
                return -1;

            _rolePageRepository.Delete(info);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> UpdatePermission(Guid roleId, int pageId, int permissions)
        {
            var rolePageInfo = await _rolePageRepository.GetAsync(false, x => x.RoleId == roleId && x.PageId == pageId);
            if (rolePageInfo != null)
            {
                rolePageInfo.Permissions = permissions;
                return await Context.SaveChangesAsync();
            }

            _rolePageRepository.Create(new RolePage(roleId, pageId, permissions));
            return await Context.SaveChangesAsync();
        }

        public async Task<bool> CheckExists(int pageId, Guid roleId)
        {
            return await _rolePageRepository.ExistAsync(x =>
                x.PageId == pageId && x.RoleId == roleId);
        }

        public async Task<List<RolePage>> GetsByPageId(int pageId, bool isReadOnly = false)
        {
            return await _rolePageRepository.GetsAsync(true, x => x.PageId == pageId);
        }

        public async Task<List<RolePage>> GetsByRoleId(Guid roleId, bool isReadOnly = false)
        {
            return await _rolePageRepository.GetsAsync(true, x => x.RoleId == roleId);
        }

        public async Task<ActionResultResponse> DeleteByPageId(int pageId)
        {
            var listRolesPages = await GetsByPageId(pageId);
            if (listRolesPages == null || !listRolesPages.Any())
                return new ActionResultResponse(-1, _resourceService.GetString("List role page by pageId does not exists."));

            _rolePageRepository.Deletes(listRolesPages);
            var result = await Context.SaveChangesAsync();
            return new ActionResultResponse(result, result > 0 ? _resourceService.GetString("Delete role page by pageId successful.")
                : _sharedResourceService.GetString("Something went wrong. Please contact with administrator."));
        }

        public async Task<int> DeleteByRoleIdAndPageIds(Guid roleId, List<int> pageId)
        {
            var listRolePages =
                await _rolePageRepository.GetsAsync(false, x => x.RoleId == roleId && pageId.Contains(x.PageId));
            if (listRolePages == null || !listRolePages.Any())
                return -1;

            _rolePageRepository.Deletes(listRolePages);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> UpdateRolePagePermission(Guid roleId, List<RolePagePermissionMeta> rolePagePermissions)
        {
            var listRolePages = await _rolePageRepository.GetsAsync(false, x => x.RoleId == roleId
                                                                                && rolePagePermissions
                                                                                    .Select(rp => rp.PageId)
                                                                                    .Contains(x.PageId));
            if (listRolePages == null || !listRolePages.Any())
                return -1;

            foreach (var rolePage in listRolePages)
            {
                var rolePagePermission = rolePagePermissions.FirstOrDefault(x => x.PageId == rolePage.PageId);
                if (rolePagePermission == null)
                    continue;

                rolePage.Permissions = rolePagePermission.Permissions;
            }
            return await Context.SaveChangesAsync();
        }

        public async Task<ActionResultResponse> DeleteByRoleId(Guid roleId)
        {
            var listRolesPages = await GetsByRoleId(roleId);
            if (listRolesPages == null || !listRolesPages.Any())
                return new ActionResultResponse(-1, _resourceService.GetString("List role page by roleId does not exists."));

            _rolePageRepository.Deletes(listRolesPages);
            var result = await Context.SaveChangesAsync();
            return new ActionResultResponse(result, result > 0 ? _resourceService.GetString("Delete role page by roleId successful.")
                : _sharedResourceService.GetString("Something went wrong. Please contact with administrator."));
        }

        public async Task<int> ForceDeleteByRoleId(Guid roleId)
        {
            var rolePages = await _rolePageRepository.GetsAsync(false, x => x.RoleId == roleId);
            if (rolePages == null || !rolePages.Any())
                return -1;

            _rolePageRepository.Deletes(rolePages);
            return await Context.SaveChangesAsync();
        }

        public async Task<RolePage> GetInfo(Guid roleId, int pageId)
        {
            return await _rolePageRepository.GetAsync(true, x => x.RoleId == roleId && x.PageId == pageId);
        }
    }
}
