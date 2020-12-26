using LIS.Core.Application.ViewModels;
using LIS.Infrastructure.Models;
using LIS.Infrastructure.ViewModel;
using MediatR;

namespace LIS.Core.Application.Queries.TenantQuery
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
