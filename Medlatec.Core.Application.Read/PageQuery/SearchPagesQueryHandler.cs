using Dapper;
using MediatR;
using Medlatec.Core.Application.Queries.PageQuery;
using Medlatec.Core.Application.ViewModels;
using Medlatec.Infrastructure.Extensions;
using Medlatec.Infrastructure.Helpers;
using Medlatec.Infrastructure.Utils;
using Medlatec.Infrastructure.ViewModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medlatec.Core.Application.Read.PageQuery
{
    public class SearchPagesQueryHandler : IRequestHandler<SearchPagesQuery, SearchResult<PageSearchViewModel>>
    {
        private readonly IDbConnection _connection;
        public SearchPagesQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<SearchResult<PageSearchViewModel>> Handle(SearchPagesQuery request, CancellationToken cancellationToken)
        {
            var sqlBuilder = new SqlBuilder();
            var tmplQueryItems = sqlBuilder.AddTemplate(@"OPEN :rslt1 FOR select p.* from ""Pages"" p
                /**where**/ /**orderby**/ offset :Skip rows fetch next :PageSize row only");
            var tmplQueryCount = sqlBuilder.AddTemplate(@"OPEN :rslt2 FOR select count(p.""Id"") from ""Pages"" p /**where**/");
            sqlBuilder.Where(@"p.""IsDelete"" = 0");
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                sqlBuilder.Where(@"p.""UnsignName"" like '%:Keyword%'");
            }

            if(request.IsActive.HasValue)
            {
                sqlBuilder.Where(@"p.""IsActive"" = :IsActive");
            }

            sqlBuilder.OrderBy(SqlUtils.Order("p", "-CreatedDate", request.Sort));

            var queryStr = $@" BEGIN
                {tmplQueryItems.RawSql};
                {tmplQueryCount.RawSql}; END;";

            OracleDynamicParameters dynParams = new OracleDynamicParameters();
            dynParams.Add(":rslt1", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":rslt2", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":Skip", OracleDbType.Int32, ParameterDirection.Input, request.Skip);
            dynParams.Add(":PageSize", OracleDbType.Int32, ParameterDirection.Input, request.PageSize);
            dynParams.Add(":Keyword", OracleDbType.NVarchar2, ParameterDirection.Input, request.Keyword.NormalizeForSearch());
            dynParams.Add(":Sort", OracleDbType.NVarchar2, ParameterDirection.Input, request.Sort);
            dynParams.Add(":IsActive", OracleDbType.Boolean, ParameterDirection.Input, request.IsActive);

            var queryResults = await _connection.QueryMultipleAsync(queryStr, dynParams);
            var items = queryResults.Read<PageSearchViewModel>();
            var totalRows = queryResults.ReadFirst<int>();
            return new SearchResult<PageSearchViewModel>
            {
                Items = items.ToList(),
                TotalRows = totalRows
            };
        }
    }
}
