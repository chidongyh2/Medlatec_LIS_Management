using LIS.Core.Application.ViewModels;
using LIS.Infrastructure.Models;
using LIS.Infrastructure.ViewModel;
using MediatR;
using System;

namespace LIS.Core.Application.Queries.RoleQuery
{
    public class SearchRoleQuery : SearchQuery, IRequest<SearchResult<RolesPagesViewModel>>
    {
        public Guid TenantId { get; private set; }
        public SearchRoleQuery(Guid tenantId, int page, int pageSize)
        {
            TenantId = tenantId;
            Page = page;
            PageSize = pageSize;
        }
    }
}
