using MediatR;
using Medlatec.Core.Application.ViewModels;
using Medlatec.Infrastructure.Models;

namespace Medlatec.Core.Application.Queries.PageQuery
{
    public class GetPageDetailQuery : IRequest<ActionResultResponse<PageDetailViewModel>>
    {
        public int Id { get; private set; }
        public GetPageDetailQuery(int id)
        {
            Id = id;
        }
    }
}
