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
using Medlatec.Infrastructure.SeedWorks;

namespace Medlatec.Core.Application.Write.Pages
{
    public class DeletePageCommandHandler : IRequestHandler<DeletePageCommand, ActionResultResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly IPageRepository _pageRepository;
        private readonly IRolePageRepository _rolePageRepository;
        private readonly IResourceService<SharedResource> _sharedResourceService;
        private readonly IResourceService<CoreResource> _resourceService;
        public DeletePageCommandHandler(IPageRepository pageRepository, IUnitOfWork uow,
            IResourceService<SharedResource> sharedResourceService, IResourceService<CoreResource> resourceService,
            IRolePageRepository rolePageRepository)
        {
            _uow = uow;
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

            page.Delete();
            await _rolePageRepository.DeleteRoleByPageId(request.Id);
            _pageRepository.Update(page);   

            var result = await _uow.SaveChangesAsync(cancellationToken);
            return new ActionResultResponse(result > 0 ? 200 : -1,
                result > 0 ? _resourceService.GetString("Delete page successful.")
                : _sharedResourceService.GetString(ErrorMessage.SomethingWentWrong));
        }
    }
}
