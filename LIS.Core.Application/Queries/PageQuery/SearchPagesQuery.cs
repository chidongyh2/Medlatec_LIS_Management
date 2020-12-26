using LIS.Core.Application.ViewModels;
using LIS.Infrastructure.Models;
using LIS.Infrastructure.ViewModel;
using MediatR;
using System;

namespace LIS.Core.Application.Queries.PageQuery
{
    public class SearchPagesQuery : SearchQuery, IRequest<SearchResult<PageSearchViewModel>>
    {
        public Guid TenantId { get; private set; }
        public bool IsActive { get; private set; }
        public SearchPagesQuery(Guid tenantId, int page, int pageSize, string keyword)
        {
            TenantId = tenantId;
            Page = page;
            PageSize = pageSize;
            Keyword = keyword;
        }
    }
}
