using Medlatec.Core.Application.ViewModels;
using Medlatec.Infrastructure.Models;
using Medlatec.Infrastructure.ViewModel;
using MediatR;

namespace Medlatec.Core.Application.Queries.TenantQuery
{
    public class SearchTenantQuery : SearchQuery, IRequest<SearchResult<TenantSearchViewModel>>
    {
        public bool? IsActive { get; private set; }
        public SearchTenantQuery(bool? isActive, int page, int pageSize)
        {
            IsActive = isActive;
            Page = page;
            PageSize = pageSize;
        }
    }
}
