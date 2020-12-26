using LIS.Core.Application.Commands.Roles;
using LIS.Core.Domain.IRepository;
using LIS.Core.Domain.Resources;
using LIS.Infrastructure.Constants;
using LIS.Infrastructure.Domain.AccountAggregate;
using LIS.Infrastructure.IServices;
using LIS.Infrastructure.Models;
using LIS.Infrastructure.Resources;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LIS.Core.Application.Write.Roles
{
    public class InsertRoleCommandHandler : IRequestHandler<InsertRoleCommand, ActionResultResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IResourceService<SharedResource> _sharedResourceService;
        private readonly IResourceService<CoreResource> _resourceService;
        public InsertRoleCommandHandler(IRoleRepository roleRepository,
            IResourceService<SharedResource> sharedResourceService, IResourceService<CoreResource> resourceService)
        {
            _roleRepository = roleRepository;
            _sharedResourceService = sharedResourceService;
            _resourceService = resourceService;
        }
        public async Task<ActionResultResponse> Handle(InsertRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.RoleMeta.RolePagePermissions == null || !request.RoleMeta.RolePagePermissions.Any())
                return new ActionResultResponse(-1, _resourceService.GetString("Please select at least 1 page and permission."));

            var roleId = Guid.NewGuid();
            // Check RoleName exists.
            var isNameExists = await _roleRepository.CheckExists(request.TenantId, roleId, request.RoleMeta.Name);
            if (isNameExists)
                return new ActionResultResponse(-2, _resourceService.GetString("Role name already exists. Please try again."));

            var role = new Role(roleId, request.TenantId, request.RoleMeta.Name, request.RoleMeta.Description);

            // Add new role pages.
            role.SetRolePages( request.TenantId, request.RoleMeta.RolePagePermissions);

            // Add new user role.
            if (request.RoleMeta.UserIds != null && request.RoleMeta.UserIds.Any())
            {
                role.InsertUsersRole(request.RoleMeta.UserIds);
            }

            var result = await _roleRepository.CreateAsync(role, new CancellationToken());

            if (result != null)
                return new ActionResultResponse(200, _resourceService.GetString("Add new role successful."));

            return new ActionResultResponse(-1, _sharedResourceService.GetString(ErrorMessage.SomethingWentWrong));
        }
    }
}
