using LIS.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIS.Core.Application.Commands.Roles
{
    public class UpdateRolePagePermissionCommand : IRequest<ActionResultResponse>
    {
        public Guid TenantId { get; private set; }
        public Guid RoleId { get; private set; }
        public int PageId { get; private set; }
        public int Permissions { get; private set; }

        public UpdateRolePagePermissionCommand(Guid tenantId, Guid roleId, int pageId, int permissions)
        {
            TenantId = tenantId;
            RoleId = roleId;
            PageId = pageId;
            Permissions = permissions;
        }
    }
}
