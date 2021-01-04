using Medlatec.Core.Application.Commands.Pages;
using Medlatec.Core.Domain.AggregateModels.TenantAggregate;
using Medlatec.Core.Domain.IRepository;
using Medlatec.Core.Domain.Resources;
using Medlatec.Infrastructure.IServices;
using Medlatec.Infrastructure.Models;
using Medlatec.Infrastructure.Resources;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medlatec.Core.Application.Write.Pages
{
    public class InsertPageCommandHandler : IRequestHandler<InsertPageCommand, ActionResultResponse>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IResourceService<SharedResource> _sharedResourceService;
        private readonly IResourceService<CoreResource> _resourceService;
        public InsertPageCommandHandler(IPageRepository pageRepository,
            IResourceService<SharedResource> sharedResourceService, IResourceService<CoreResource> resourceService)
        {
            _pageRepository = pageRepository;
            _sharedResourceService = sharedResourceService;
            _resourceService = resourceService;
        }
        public async Task<ActionResultResponse> Handle(InsertPageCommand request, CancellationToken cancellationToken)
        {
            // Check Id exists.
            var isIdExists = await _pageRepository.CheckExists(request.Id);
            if (isIdExists)
                return new ActionResultResponse(-1, _resourceService.GetString("Page already exists."));

            var page = new Page(request.Id, request.Name, request.Description, request.Icon.Trim(), request.Order , -1, request.Url, request.IsActive);

            if (request.ParentId.HasValue)
            {
                var parentInfo = await _pageRepository.GetInfo(request.ParentId.Value, true);
                if (parentInfo == null)
                    return new ActionResultResponse(-3, _resourceService.GetString("Parent page does not exists."));

                page.SetParent(parentInfo.Id);
            }
            #region Update page idPath.

            //page.IdPath = page.IdPath.Replace("-1", page.Id.ToString());
            //await _pageRepository.Update(page);
            #endregion
            if (request.TenantIds != null && request.TenantIds.Any())
            {
                page.SetTenantsPage(request.TenantIds);
            }

            var resultInsertPage = await _pageRepository.Insert(page);
            if (resultInsertPage <= 0)
                return new ActionResultResponse(resultInsertPage, _sharedResourceService.GetString("Something went wrong. Please contact with administrator."));

            return new ActionResultResponse(resultInsertPage, _resourceService.GetString("Add new page successful."));
        }
    }
}
