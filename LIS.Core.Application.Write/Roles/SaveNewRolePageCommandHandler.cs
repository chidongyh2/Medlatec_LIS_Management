using LIS.Core.Application.Commands.Roles;
using LIS.Core.Domain.IRepository;
using LIS.Core.Domain.Resources;
using LIS.Infrastructure.Domain.AccountAggregate;
using LIS.Infrastructure.IServices;
using LIS.Infrastructure.Models;
using LIS.Infrastructure.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LIS.Core.Application.Write.Roles
{
    public class SaveNewRolePageCommandHandler : IRequestHandler<SaveNewRolePageCommand, ActionResultResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRolePageRepository _rolePageRepository;
        private readonly IResourceService<SharedResource> _sharedResourceService;
        private readonly IResourceService<CoreResource> _resourceService;
        public SaveNewRolePageCommandHandler(IRoleRepository roleRepository, IRolePageRepository rolePageRepository,
            IResourceService<SharedResource> sharedResourceService, IResourceService<CoreResource> resourceService)
        {
            _roleRepository = roleRepository;
            _sharedResourceService = sharedResourceService;
            _resourceService = resourceService;
            _rolePageRepository = rolePageRepository;
        }
        public async Task<ActionResultResponse> Handle(SaveNewRolePageCommand request, CancellationToken cancellationToken)
        {
            var roleInfo = await _roleRepository.FindByIdAsync(request.RoleId.ToString(), new CancellationToken());
            if (roleInfo == null)
                return new ActionResultResponse(-1, _resourceService.GetString("Role does not exists."));

            if (roleInfo.TenantId != request.TenantId)
                return new ActionResultResponse(-2, _sharedResourceService.GetString("You do not have permission to do this action."));

            roleInfo.SetRolePages(request.TenantId, request.RolePagePermissionMeta);

            var result = await _roleRepository.UpdateAsync(roleInfo, new CancellationToken());

            return new ActionResultResponse(result.Succeeded ? 200 : -1, !result.Succeeded ? _sharedResourceService.GetString("Something went wrong. Please contact with administrator.")
                : _resourceService.GetString("Add new role permission successful."));
        }
    }
}
