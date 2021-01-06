using Dapper;
using MediatR;
using Medlatec.Core.Application.Queries.PageQuery;
using Medlatec.Core.Application.ViewModels;
using Medlatec.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medlatec.Core.Application.Read.PageQuery
{
    public class GetFullPagesTreeQueryHandler : IRequestHandler<GetFullPagesTreeQuery, IList<TreeData>>
    {
        private readonly IDbConnection _connection;
        public GetFullPagesTreeQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IList<TreeData>> Handle(GetFullPagesTreeQuery request, CancellationToken cancellationToken)
        {
            var tree = new List<TreeData>();
            var query = @"select * from ""Pages"" WHERE ""IsActive"" = 1 and ""IsDelete"" = 0 ORDER BY ""IdPath"", ""OrderPath""";
            var pages = await _connection.QueryAsync<PageSearchViewModel>(query);
            if (pages == null || !pages.Any())
                return tree;

            tree = RenderPageTree(pages.ToList(), null);
            return tree;
        }

        private List<TreeData> RenderPageTree(List<PageSearchViewModel> pages, int? parentId)
        {
            var pageTree = new List<TreeData>();
            var listPages = pages.Where(x => x.ParentId == parentId).ToList();
            if (!listPages.Any()) return pageTree;

            pageTree.AddRange(listPages.Select(page => new TreeData
            {
                Id = page.Id,
                Text = page.Name,
                Icon = page.Icon,
                ParentId = parentId,
                IdPath = page.IdPath,
                Data = page,
                Children = RenderPageTree(pages, page.Id),
                State = new State()
            }));
            return pageTree;
        }
    }
}
