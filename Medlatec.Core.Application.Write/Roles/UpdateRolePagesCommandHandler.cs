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
    public class UpdateRolePagesCommandHandler : IRequestHandler<UpdateRolePagesCommand, ActionResultResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRolePageRepository _rolePageRepository;
        private readonly IResourceService<SharedResource> _sharedResourceService;
        private readonly IResourceService<CoreResource> _resourceService;
        public UpdateRolePagesCommandHandler(IRoleRepository roleRepository, IRolePageRepository rolePageRepository,
            IResourceService<SharedResource> sharedResourceService, IResourceService<CoreResource> resourceService)
        {
            _roleRepository = roleRepository;
            _sharedResourceService = sharedResourceService;
            _resourceService = resourceService;
            _rolePageRepository = rolePageRepository;
        }
        public async Task<ActionResultResponse> Handle(UpdateRolePagesCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
