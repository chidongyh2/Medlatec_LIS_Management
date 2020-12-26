using LIS.Core.Application.Commands.Roles;
using LIS.Core.Domain.IRepository;
using LIS.Core.Domain.Resources;
using LIS.Infrastructure.Constants;
using LIS.Infrastructure.IServices;
using LIS.Infrastructure.Models;
using LIS.Infrastructure.Resources;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LIS.Core.Application.Write.Roles
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ActionResultResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRolePageRepository _rolePageRepository;
        private readonly IResourceService<SharedResource> _sharedResourceService;
        private readonly IResourceService<CoreResource> _resourceService;
        public UpdateRoleCommandHandler(IRoleRepository roleRepository, IRolePageRepository rolePageRepository,
           IResourceService<SharedResource> sharedResourceService, IResourceService<CoreResource> resourceService)
        {
            _roleRepository = roleRepository;
            _sharedResourceService = sharedResourceService;
            _resourceService = resourceService;
            _rolePageRepository = rolePageRepository;
        }
        public async Task<ActionResultResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.RoleMeta.RolePagePermissions == null || !request.RoleMeta.RolePagePermissions.Any())
                return new ActionResultResponse(-1, _resourceService.GetString("Please select at least 1 page and permission."));

            // Check RoleName exists.
            var isNameExists = await _roleRepository.CheckExists(request.TenantId, request.Id, request.RoleMeta.Name?.Trim());
            if (isNameExists)
                return new ActionResultResponse(-2, _resourceService.GetString("Role name already exists. Please try again."));

            var roleInfo = await _roleRepository.FindByIdAsync(request.Id.ToString(), new CancellationToken());
            if (roleInfo == null)
                return new ActionResultResponse(-3, _resourceService.GetString("Role does not exists. Please try again."));

            if (roleInfo.TenantId != request.TenantId)
                return new ActionResultResponse(-4, _sharedResourceService.GetString(ErrorMessage.NotHavePermission));

            if (request.RoleMeta.ConcurrencyStamp != roleInfo.ConcurrencyStamp)
                return new ActionResultResponse(-5, _resourceService.GetString("Role has been updated by another staff. You can not update this role."));
         
            // Save role infomation
            roleInfo.UpdateInfo(request.RoleMeta.Name?.Trim(), request.RoleMeta.Description?.Trim());

            // Save role pages.
            roleInfo.SetRolePages(request.TenantId, request.RoleMeta.RolePagePermissions);

            roleInfo.InsertNewUsersRole(request.RoleMeta.UserIds);


            var result = await _roleRepository.UpdateAsync(roleInfo, new CancellationToken());

            return new ActionResultResponse(result.Succeeded ? 1 : 0, result.Succeeded
                ? _resourceService.GetString("Update role successful.")
                : _sharedResourceService.GetString("Something went wrong. Please contact with administrator."));
        }
    }
}
