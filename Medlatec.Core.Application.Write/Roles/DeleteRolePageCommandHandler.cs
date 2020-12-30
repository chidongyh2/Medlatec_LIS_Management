using Medlatec.Core.Application.Commands.Roles;
using Medlatec.Core.Domain.IRepository;
using Medlatec.Core.Domain.Resources;
using Medlatec.Infrastructure.IServices;
using Medlatec.Infrastructure.Models;
using Medlatec.Infrastructure.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medlatec.Core.Application.Write.Roles
{
    public class DeleteRolePageCommandHandler : IRequestHandler<DeleteRolePageCommand, ActionResultResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRolePageRepository _rolePageRepository;
        private readonly IResourceService<SharedResource> _sharedResourceService;
        private readonly IResourceService<CoreResource> _resourceService;
        public DeleteRolePageCommandHandler(IRoleRepository roleRepository, IRolePageRepository rolePageRepository,
            IResourceService<SharedResource> sharedResourceService, IResourceService<CoreResource> resourceService)
        {
            _roleRepository = roleRepository;
            _sharedResourceService = sharedResourceService;
            _resourceService = resourceService;
            _rolePageRepository = rolePageRepository;
        }
        public async Task<ActionResultResponse> Handle(DeleteRolePageCommand request, CancellationToken cancellationToken)
        {
            var roleInfo = await _roleRepository.FindByIdAsync(request.RoleId.ToString(), new CancellationToken());
            if (roleInfo == null)
                return new ActionResultResponse(-1, _resourceService.GetString("Role does not exists."));

            if (roleInfo.TenantId != request.TenantId)
                return new ActionResultResponse(-403, _sharedResourceService.GetString("You do not have permission to do this action."));

            var result = await _rolePageRepository.Delete(request.RoleId, request.PageId);
            return new ActionResultResponse(result, result == -1
                ? _resourceService.GetString("Remove permission fail.")
                : result == 0
                    ? _sharedResourceService.GetString("Nothing updated.")
                    : _resourceService.GetString("Remove permission successful."));
        }
    }
}
