using Medlatec.Core.Application.ModelMetas;
using Medlatec.Infrastructure.Models;
using MediatR;
using System;

namespace Medlatec.Core.Application.Commands.Roles
{
    public class UpdateRoleCommand : IRequest<ActionResultResponse>
    {
        public Guid TenantId { get; private set; }
        public Guid Id { get; private set; }
        public RoleMeta RoleMeta { get; private set; }
        public UpdateRoleCommand(Guid tenantId, Guid id, RoleMeta roleMeta)
        {
            TenantId = tenantId;
            Id = id;
            RoleMeta = roleMeta;
        }
    }
}
