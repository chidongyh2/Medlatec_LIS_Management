using Medlatec.Core.Application.ViewModels;
using Medlatec.Infrastructure.Models;
using Medlatec.Infrastructure.ViewModel;
using MediatR;
using System;

namespace Medlatec.Core.Application.Queries.PageQuery
{
    public class SearchPagesQuery : SearchQuery, IRequest<SearchResult<PageSearchViewModel>>
    {
        public bool? IsActive { get; private set; }
        public SearchPagesQuery(string keyword, string sort, int page, int pageSize, bool? isActive)
        {
            Page = page;
            PageSize = pageSize;
            Keyword = keyword;
            Sort = sort;
            IsActive = isActive;
        }
    }
}
