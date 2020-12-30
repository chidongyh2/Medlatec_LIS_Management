using Medlatec.Infrastructure.Models;
using MediatR;
using System;

namespace Medlatec.Core.Application.Commands.UserAccounts
{
    public class DeleteUserRoleCommand : IRequest<ActionResultResponse>
    {
        public Guid TenantId { get; private set; }
        public Guid UserId { get; private set; }
        public DeleteUserRoleCommand(Guid tenantId, Guid userId)
        {
            TenantId = tenantId;
            UserId = userId;
        }
    }
}
