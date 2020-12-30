using Medlatec.Core.Application.ModelMetas;
using Medlatec.Infrastructure.Models;
using MediatR;
using System;

namespace Medlatec.Core.Application.Commands.Roles
{
    public class InsertRoleCommand : IRequest<ActionResultResponse>
    {
        public Guid TenantId { get; private set; }
        public RoleMeta RoleMeta { get; private set; }
        public InsertRoleCommand(Guid tenantId, RoleMeta roleMeta)
        {
            TenantId = tenantId;
            RoleMeta = roleMeta;
        }
    }
}
