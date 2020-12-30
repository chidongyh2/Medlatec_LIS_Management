using Medlatec.Infrastructure.SeedWorks;
using System;
using System.ComponentModel.DataAnnotations;

namespace Medlatec.Infrastructure.Domain.AccountAggregate
{
    public class RolePage : Entity
    {
        public Guid RoleId { get; set; }
        public int PageId { get; set; }
        [Required]
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
