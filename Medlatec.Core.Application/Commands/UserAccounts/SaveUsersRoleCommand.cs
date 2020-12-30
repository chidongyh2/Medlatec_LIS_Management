using Medlatec.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Medlatec.Core.Application.Commands.UserAccounts
{
    public class SaveUsersRoleCommand : IRequest<ActionResultResponse>
    {
        public Guid TenantId { get; private set; }
        public Guid RoleId { get; private set; }
        public IList<Guid> UserIds { get; private set; }
        public SaveUsersRoleCommand(Guid tenantId, Guid roleId, IList<Guid> userIds)
        {
            TenantId = tenantId;
            RoleId = roleId;
            UserIds = userIds;
        }
    }
}
