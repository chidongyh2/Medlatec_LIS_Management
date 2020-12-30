using Medlatec.Core.Application.Commands.Roles;
using Medlatec.Core.Domain.IRepository;
using Medlatec.Core.Domain.Resources;
using Medlatec.Infrastructure.IServices;
using Medlatec.Infrastructure.Models;
using Medlatec.Infrastructure.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Medlatec.Core.Application.Write.Roles
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ActionResultResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRolePageRepository _rolePageRepository;
        private readonly IResourceService<SharedResource> _sharedResourceService;
        private readonly IResourceService<CoreResource> _resourceService;
        public DeleteRoleCommandHandler(IRoleRepository roleRepository, IRolePageRepository rolePageRepository,
            IResourceService<SharedResource> sharedResourceService, IResourceService<CoreResource> resourceService)
        {
            _roleRepository = roleRepository;
            _sharedResourceService = sharedResourceService;
            _resourceService = resourceService;
            _rolePageRepository = rolePageRepository;
        }
        public async Task<ActionResultResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var roleInfo = await _roleRepository.FindByIdAsync(request.Id.ToString(), new CancellationToken());
            if (roleInfo == null)
                return new ActionResultResponse(-1, _resourceService.GetString("Role does not exists. Please try again."));

            if (roleInfo.TenantId != request.TenantId)
                return new ActionResultResponse(-2, _sharedResourceService.GetString("You do not have permission to do this action."));

            var result = await _roleRepository.DeleteAsync(roleInfo, new CancellationToken());
            if (result.Succeeded)
            {
                // TODO: Check this later.
                // Delete user role and role page by NServiceBus Event.
                //await _messageSession.Publish(new RoleDeleted(roleInfo.Id))
                //    .ConfigureAwait(false);

                await _rolePageRepository.DeleteByRoleId(roleInfo.Id);
            }

            return new ActionResultResponse(result.Succeeded ? 1 : 0,
                result.Succeeded ? _resourceService.GetString("Delete role successful.")
                    : _sharedResourceService.GetString("Something went wrong. Please contact with administrator."));
        }
    }
}
