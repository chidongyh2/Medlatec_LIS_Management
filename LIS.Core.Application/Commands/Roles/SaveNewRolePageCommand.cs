using LIS.Infrastructure.ModelMetas;
using LIS.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace LIS.Core.Application.Commands.Roles
{
    public class SaveNewRolePageCommand : IRequest<ActionResultResponse>
    {
        public Guid TenantId { get; private set; }
        public Guid RoleId { get; private set; }
        public List<RolePagePermissionMeta> RolePagePermissionMeta { get; private set; }
        public SaveNewRolePageCommand(Guid tenantId, Guid roleId, List<RolePagePermissionMeta> rolePagePermissionMeta)
        {
            TenantId = tenantId;
            RoleId = roleId;
            RolePagePermissionMeta = rolePagePermissionMeta;
        }
    }
}
