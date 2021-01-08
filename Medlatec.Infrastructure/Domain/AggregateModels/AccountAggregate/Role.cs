using Medlatec.Infrastructure.Constants;
using Medlatec.Infrastructure.Extensions;
using Medlatec.Infrastructure.ModelMetas;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Medlatec.Infrastructure.Domain.AccountAggregate
{
    public class Role : IdentityRole<Guid>
    {
        public Guid TenantId { get; private set; }
        [MaxLength(500)]
        public string Description { get; private set; }
        [MaxLength(250)]
        public string Type { get; private set; }
        public virtual ISet<RolePage> RolePages { get; private set; }
        public virtual ISet<AccountRole> AccountRoles { get; private set; }
        public Role(Guid id, Guid tenantId, string name, string description, string type = AuthRole.Administrator)
        {
            TenantId = tenantId;
            Name = name;
            Description = description;
            NormalizedName = name.Trim().StripVietnameseChars().ToUpper();
            Description = description?.Trim();
            TenantId = tenantId;
            Type = type;
            ConcurrencyStamp = id.ToString();
            Id = id;
        }

        public Role(string type, string name, string description)
        {
            Name = name;
            Description = description;
            NormalizedName = name.Trim().StripVietnameseChars().ToUpper();
            Description = description?.Trim();
            Type = type;
        }

        public void SetRolePages(Guid tenantId, IList<RolePagePermissionMeta> rolePagePermissions)
        {
            rolePagePermissions ??= new List<RolePagePermissionMeta>();
            RolePages ??= new HashSet<RolePage>();
            var toBeAddedIds = rolePagePermissions.Where(page => RolePages.All(t => t.PageId != page.PageId));
            var toBeDeleted = RolePages.Where(t => !(rolePagePermissions.Select(x => x.PageId)).Contains(t.PageId));
            RolePages.RemoveRange(toBeDeleted);
            RolePages.AddRange(toBeAddedIds.Select(page => new RolePage(tenantId, page.PageId, page.Permissions)));
        }

        public void UpdateInfo(string name, string description)
        {
            Name = name;
            Description = description;
            NormalizedName = name.Trim().StripVietnameseChars().ToUpper();
            Description = description?.Trim();
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }

        public void InsertUsersRole(IReadOnlyCollection<Guid> userIds)
        {
            userIds ??= new List<Guid>();
            AccountRoles ??= new HashSet<AccountRole>();
            var toBeAddedIds = userIds.Where(userId => AccountRoles.All(t => t.UserId != userId));
            AccountRoles.AddRange(toBeAddedIds.Select(userId => new AccountRole(userId, Id)));
        }

        public void InsertNewUsersRole(IReadOnlyCollection<Guid> userIds)
        {
            userIds ??= new List<Guid>();
            AccountRoles = new HashSet<AccountRole>();
            var toBeAddedIds = userIds.Where(userId => AccountRoles.All(t => t.UserId != userId));
            AccountRoles.AddRange(toBeAddedIds.Select(userId => new AccountRole(userId, Id)));
        }
    }
}
