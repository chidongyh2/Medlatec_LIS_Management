using Dapper;
using MediatR;
using Medlatec.Core.Application.Queries.PageQuery;
using Medlatec.Core.Application.ViewModels;
using Medlatec.Core.Domain.Resources;
using Medlatec.Infrastructure.IServices;
using Medlatec.Infrastructure.Models;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Medlatec.Core.Application.Read.PageQuery
{
    public class GetPageDetailQueryHandler : IRequestHandler<GetPageDetailQuery, ActionResultResponse<PageDetailViewModel>>
    {
        private IDbConnection _connection;
        private readonly IResourceService<CoreResource> _resourceService;
        public GetPageDetailQueryHandler(IDbConnection connection, IResourceService<CoreResource> resourceService)
        {
            _connection = connection;
            _resourceService = resourceService;
        }
        public async Task<ActionResultResponse<PageDetailViewModel>> Handle(GetPageDetailQuery request, CancellationToken cancellationToken)
        {
            var item = await _connection.QueryFirstOrDefaultAsync(@"select  * from ""Pages"" WHERE ""IsDelete"" = 0 AND ""Id"" == :Id", new { request.Id });
            if (item != null)
                return new ActionResultResponse<PageDetailViewModel>
                {
                    Data = item
                };
            else
                return new ActionResultResponse<PageDetailViewModel>(-1, _resourceService.GetString("Page not found."));
        }
    }
}
