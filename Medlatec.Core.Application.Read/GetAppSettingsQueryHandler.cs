using Dapper;
using MediatR;
using Medlatec.Core.Application.Queries;
using Medlatec.Core.Application.ViewModels;
using Medlatec.Infrastructure.Helpers;
using Medlatec.Infrastructure.ViewModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medlatec.Core.Application.Read
{
    public class GetAppSettingsQueryHandler : IRequestHandler<GetAppSettingsQuery, AppSettingViewModel>
    {
        private readonly IDbConnection _connection;
        public GetAppSettingsQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<AppSettingViewModel> Handle(GetAppSettingsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sqlBuilder = new SqlBuilder();
                var queryPages = sqlBuilder.AddTemplate(@"OPEN :rslt1 FOR select p.* from ""Pages"" p
                join ""TenantPages"" tp on p.""Id"" = tp.""PageId"" where p.""IsDelete"" = 0 and tp.""IsDelete"" = 0 and p.""IsActive"" = 1");

                var queryPermission = sqlBuilder.AddTemplate(@"OPEN :rslt2 FOR select ur.""RoleId"", rp.""PageId"", rp.""Permissions""
                from ""AspNetUserRoles"" ur left join ""RolePages"" rp on ur.""RoleId"" = rp.""RoleId"" where ur.""UserId"" = :UserId");
                var query = $@"BEGIN {queryPages.RawSql}; {queryPermission.RawSql}; END;";
                OracleDynamicParameters dynParams = new OracleDynamicParameters();
                dynParams.Add(":rslt1", OracleDbType.RefCursor, ParameterDirection.Output);
                dynParams.Add(":rslt2", OracleDbType.RefCursor, ParameterDirection.Output);
                dynParams.Add(":UserId", OracleDbType.Raw, ParameterDirection.Input, request.UserId);
                var appSettings = new AppSettingViewModel();
                var result = await _connection.QueryMultipleAsync(query, dynParams);
                appSettings.Pages = result.Read<PageGetByUserViewModel>().ToList();
                appSettings.Permissions = result.Read<RolesPagesViewModel>().ToList();
                return appSettings;
            } catch (Exception e)
            {
                throw e;
            }

        }
    }
}
