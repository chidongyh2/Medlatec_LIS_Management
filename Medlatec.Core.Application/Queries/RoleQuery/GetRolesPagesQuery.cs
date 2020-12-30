using Medlatec.Core.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace Medlatec.Core.Application.Queries.RoleQuery
{
    public class GetRolesPagesQuery : IRequest<List<RolesPagesViewModel>>
    {
        public Guid TenantId { get; private set; }
        public Guid RoleId { get; private set; }
        public GetRolesPagesQuery(Guid tennatId, Guid roleId) {
            TenantId = tennatId;
            RoleId = roleId;
        }
    }
}
