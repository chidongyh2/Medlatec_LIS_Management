using Dapper;
using MediatR;
using Medlatec.Core.Application.Queries.RoleQuery;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medlatec.Core.Application.Read.RoleQuery
{
    public class CheckPermissionQueryHandler : IRequestHandler<CheckPermissionQuery, bool>
    {
        private IDbConnection _connection;
        public CheckPermissionQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<bool> Handle(CheckPermissionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var PageIds = request.PagePermissions.Select(x => (int)x.PageId).ToArray();

                var query = @"select rg.""PageId"", rg.""Permissions"" from ""AspNetUserRoles"" usr
                            JOIN ""RolePages"" rg on usr.""RoleId"" = rg.""RoleId""
                            WHERE usr.""UserId"" = :UserId and rg.""PageId"" in :PageIds";
                var queryPermissions = await _connection.QueryAsync(query, new { request.UserId, PageIds });
                if (!queryPermissions.Any())
                    return false;

                foreach (var pagePermission in request.PagePermissions)
                {
                    var permissions = queryPermissions
                        .Where(x => x.PageId == (int)pagePermission.PageId)
                        .Select(x => x.Permissions).ToList();
                    if (!permissions.Any()) continue;

                    var isHasPermission = pagePermission.Permissions.Any(permission =>
                        permissions.Any(x => (x & (int)permission) == (int)permission));
                    if (isHasPermission)
                        return true;
                }
                return false;
            } catch (Exception e)
            {
                throw e;
            }
        }
    }
}
