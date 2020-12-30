using Medlatec.Core.Application.ModelMetas;
using Medlatec.Infrastructure.Models;
using MediatR;
using System;

namespace Medlatec.Core.Application.Commands.Tenants
{
    public class UpdateTenantCommand : IRequest<ActionResultResponse>
    {
        public Guid Id { get; private set; }
        public TenantMeta TenantMeta { get; private set; }
        public UpdateTenantCommand(Guid id, TenantMeta tenantMeta)
        {
            TenantMeta = tenantMeta;
            Id = id;
        }
    }
}
