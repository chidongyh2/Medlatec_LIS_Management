using LIS.Infrastructure.ModelMetas;
using System;
using System.Collections.Generic;

namespace LIS.Core.Application.ModelMetas
{
    public class RoleMeta
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ConcurrencyStamp { get; set; }
        public IReadOnlyCollection<Guid> UserIds { get; set; }
        public RolePagePermissionMeta[] RolePagePermissions { get; set; }
    }
}
