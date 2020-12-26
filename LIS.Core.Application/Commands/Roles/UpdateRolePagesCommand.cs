using LIS.Core.Application.ModelMetas;
using LIS.Infrastructure.ModelMetas;
using LIS.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace LIS.Core.Application.Commands.Roles
{
    public class UpdateRolePagesCommand : IRequest<ActionResultResponse>
    {
        public Guid RoleId { get; private set; }
        public IList<RolePagePermissionMeta> RolePagePermissions { get; private set; }
        public UpdateRolePagesCommand(Guid roleId, IList<RolePagePermissionMeta> rolePagePermissions)
        {
            RoleId = roleId;
            RolePagePermissions = rolePagePermissions;
        }
    }
}
