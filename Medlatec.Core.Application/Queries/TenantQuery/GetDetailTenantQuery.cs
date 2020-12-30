using Medlatec.Infrastructure.Models;
using MediatR;
using System;

namespace Medlatec.Core.Application.Queries.TenantQuery
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
