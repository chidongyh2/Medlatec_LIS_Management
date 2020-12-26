using LIS.Infrastructure.Models;
using MediatR;
using System;

namespace LIS.Core.Application.Queries.TenantQuery
{
    public class GetDetailTenantQuery : IRequest<ActionResultResponse>
    {
        public Guid Id { get; private set; }
        public GetDetailTenantQuery(Guid id)
        {
            Id = id;
        }
    }
}
