using Medlatec.Infrastructure.Models;
using MediatR;
using System.Collections.Generic;

namespace Medlatec.Core.Application.Queries.PageQuery
{
    public class GetFullPagesTreeQuery : IRequest<IList<TreeData>>
    {
    }
}
