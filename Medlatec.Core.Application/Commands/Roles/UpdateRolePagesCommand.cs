using Medlatec.Core.Application.ModelMetas;
using Medlatec.Infrastructure.ModelMetas;
using Medlatec.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Medlatec.Core.Application.Commands.Roles
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
