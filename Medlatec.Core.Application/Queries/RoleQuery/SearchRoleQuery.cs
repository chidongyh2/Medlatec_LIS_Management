using Medlatec.Core.Application.ViewModels;
using Medlatec.Infrastructure.Models;
using Medlatec.Infrastructure.ViewModel;
using MediatR;
using System;

namespace Medlatec.Core.Application.Queries.RoleQuery
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
