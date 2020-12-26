using LIS.Infrastructure.Models;
using MediatR;
using System;

namespace LIS.Core.Application.Commands.Roles
{
    public class DeleteRoleCommand : IRequest<ActionResultResponse>
    {
        public Guid TenantId { get; private set; }
        public Guid Id { get; private set; }
        public DeleteRoleCommand(Guid tenantId, Guid id)
        {
            TenantId = tenantId;
            Id = id;
        }
    }
}
