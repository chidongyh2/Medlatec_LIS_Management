using LIS.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIS.Core.Application.Commands.Roles
{
    public class DeleteRolePageCommand : IRequest<ActionResultResponse>
    {
        public Guid TenantId { get; private set; }
        public Guid RoleId { get; private set; }
        public int PageId { get; private set; }
        public DeleteRolePageCommand(Guid tenantId, Guid roleId, int pageId)
        {
            TenantId = tenantId;
            RoleId = roleId;
            PageId = pageId;
        }
    }
}
