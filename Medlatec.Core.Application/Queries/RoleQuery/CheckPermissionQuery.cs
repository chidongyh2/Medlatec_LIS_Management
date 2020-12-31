using MediatR;
using Medlatec.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medlatec.Core.Application.Queries.RoleQuery
{
    public class CheckPermissionQuery : IRequest<bool>
    {
        public Guid UserId { get; private set; }
        public IList<PagePermission> PagePermissions { get; private set; }
        public CheckPermissionQuery(Guid userId, IList<PagePermission> pagePermissions)
        {
            UserId = userId;
            PagePermissions = pagePermissions;
        }
    }
}
