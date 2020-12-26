using LIS.Core.Application.ViewModels;
using LIS.Infrastructure.Models;
using LIS.Infrastructure.ViewModel;
using MediatR;
using System;

namespace LIS.Core.Application.Queries.UserAccountQuery
{
    public class SearchUserAccountQuery :  SearchQuery, IRequest<SearchResult<UserAccountViewModel>>
    {
        public Guid TenantId { get; private set; }
        public bool? IsActive { get; private set; }
        public SearchUserAccountQuery(Guid tenantId, bool isActive)
        {
            TenantId = tenantId;
            IsActive = isActive;
        }
    }
}
