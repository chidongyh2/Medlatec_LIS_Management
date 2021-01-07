using Medlatec.Core.Application.Commands.Pages;
using Medlatec.Core.Domain.IRepository;
using Medlatec.Core.Domain.Resources;
using Medlatec.Infrastructure.IServices;
using Medlatec.Infrastructure.Models;
using Medlatec.Infrastructure.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Medlatec.Infrastructure.SeedWorks;

namespace Medlatec.Core.Application.Write.Pages
{
    public class UpdatePageCommandHandler : IRequestHandler<UpdatePageCommand, ActionResultResponse>
    {
        private IUnitOfWork _uow;
        private readonly IPageRepository _pageRepository;
        private readonly IResourceService<SharedResource> _sharedResourceService;
        private readonly IResourceService<CoreResource> _resourceService;
        public UpdatePageCommandHandler(IPageRepository pageRepository, IUnitOfWork uow,
            IResourceService<SharedResource> sharedResourceService, IResourceService<CoreResource> resourceService)
        {
            _pageRepository = pageRepository;
            _sharedResourceService = sharedResourceService;
            _resourceService = resourceService;
        }
        public async Task<ActionResultResponse> Handle(UpdatePageCommand request, CancellationToken cancellationToken)
        {
            var pageInfo = await _pageRepository.GetInfo(request.Id);
            if (pageInfo == null)
                return new ActionResultResponse(-1, _resourceService.GetString("Page not found."));

            var oldIdPath = pageInfo.IdPath;

            if (request.ParentId.HasValue && request.ParentId != pageInfo.ParentId)
            {
                var parentInfo = await _pageRepository.GetInfo(request.ParentId.Value);
                if (parentInfo == null)
                    return new ActionResultResponse(-3, _resourceService.GetString("Parent page does not exists."));

                pageInfo.SetParent(parentInfo.Id);
            }
            if (!request.ParentId.HasValue)
            {
                pageInfo.SetParent(null);
            }
            pageInfo.UpdateInfo(request.Name, request.Description, request.Icon.Trim(), request.Order, request.Url, request.IsActive);

            _pageRepository.Update(pageInfo);

            var resultUpdatePage = await _uow.SaveChangesAsync(cancellationToken);

            if (resultUpdatePage < 0)
                return new ActionResultResponse(resultUpdatePage, _sharedResourceService.GetString("Something went wrong. Please contact with administrator."));

            #region update children idPath
            await _pageRepository.UpdateIdPath(oldIdPath, pageInfo.IdPath);
            #endregion

            return new ActionResultResponse(1, _resourceService.GetString("Update page successful."));
        }
    }
}
