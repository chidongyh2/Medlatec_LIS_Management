using LIS.Infrastructure.SeedWorks;
using System;

namespace LIS.Infrastructure.Domain.AccountAggregate
{
    public class RolePage : Entity
    {
        public Guid RoleId { get; set; }
        public int PageId { get; set; }
        public int Permissions { get; set; }
        public virtual Role Role { get; private set; }
        public RolePage(Guid roleId, int pageId, int permissions)
        {
            RoleId = roleId;
            PageId = pageId;
            Permissions = permissions;
        }
    }
}
