using Medlatec.Core.Application.ViewModels;
using Medlatec.Infrastructure.Models;
using Medlatec.Infrastructure.ViewModel;
using MediatR;
using System;

namespace Medlatec.Core.Application.Queries.UserAccountQuery
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
