using LIS.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace LIS.Core.Application.Queries.UserAccountQuery
{
    public class CheckPagesPermissionsQuery : IRequest<bool>
    {
        public Guid UserId { get; private set; }
        public IList<PagePermission> PagePermissions { get; private set; }
        public CheckPagesPermissionsQuery(Guid userId, IList<PagePermission> pagePermissions)
        {
            UserId = userId;
            PagePermissions = pagePermissions;
        }
    }
}
