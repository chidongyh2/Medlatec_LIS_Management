using Microsoft.AspNetCore.Identity;
using System;

namespace LIS.Infrastructure.Domain.AccountAggregate
{
    public class AccountRole: IdentityUserRole<Guid>
    {
        public virtual Role Role { get; set; }
        public virtual Account Account { get; private set; }

        public AccountRole(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
