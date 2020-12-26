using LIS.Infrastructure.Models;
using MediatR;
using System.Collections.Generic;

namespace LIS.Core.Application.Queries.PageQuery
{
    public class GetFullPagesTreeQuery : IRequest<IList<TreeData>>
    {
    }
}
