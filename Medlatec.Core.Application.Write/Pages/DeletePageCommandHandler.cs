using Medlatec.Core.Application.Commands.Pages;
using Medlatec.Core.Domain.IRepository;
using Medlatec.Core.Domain.Resources;
using Medlatec.Infrastructure.IServices;
using Medlatec.Infrastructure.Models;
using Medlatec.Infrastructure.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Medlatec.Infrastructure.Constants;

namespace Medlatec.Core.Application.Write.Pages
{
    public class DeletePageCommandHandler : IRequestHandler<DeletePageCommand, ActionResultResponse>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IRolePageRepository _rolePageRepository;
        private readonly IResourceService<SharedResource> _sharedResourceService;
        private readonly IResourceService<CoreResource> _resourceService;
        public DeletePageCommandHandler(IPageRepository pageRepository,
            IResourceService<SharedResource> sharedResourceService, IResourceService<CoreResource> resourceService,
            IRolePageRepository rolePageRepository)
        {
            _pageRepository = pageRepository;
            _rolePageRepository = rolePageRepository;
            _sharedResourceService = sharedResourceService;
            _resourceService = resourceService;
        }
        public async Task<ActionResultResponse> Handle(DeletePageCommand request, CancellationToken cancellationToken)
        {
            var page = await _pageRepository.GetInfo(request.Id, false);

            if (page == null)
                return new ActionResultResponse(-1, _resourceService.GetString("Page not found."));

            page.RemoveAll();

            var result = await _rolePageRepository.DeleteRoleByPageId(request.Id);

            return new ActionResultResponse(result > 0 ? 200: -1,
                result > 0 ? _resourceService.GetString("Delete page successful.")
                : _sharedResourceService.GetString(ErrorMessage.SomethingWentWrong));
        }
    }
}
