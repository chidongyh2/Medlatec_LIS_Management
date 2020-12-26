using LIS.Infrastructure.Models;
using MediatR;
using System;

namespace LIS.Core.Application.Commands.UserAccounts
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
