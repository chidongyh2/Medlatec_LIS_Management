using Medlatec.Core.Application.Commands.Pages;
using Medlatec.Core.Domain.IRepository;
using Medlatec.Core.Domain.Resources;
using Medlatec.Infrastructure.IServices;
using Medlatec.Infrastructure.Models;
using Medlatec.Infrastructure.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
            var result = await _pageRepository.Delete(request.Id);
            if (result <= 0)
                return new ActionResultResponse(result, result == -1 ?
                    _resourceService.GetString("Page not found.")
                    : _sharedResourceService.GetString("Something went wrong. Please contact with administrator."));

            await _rolePageRepository.DeleteByPageId(request.Id);

            return new ActionResultResponse(result, _resourceService.GetString("Delete page successful."));
        }
    }
}
