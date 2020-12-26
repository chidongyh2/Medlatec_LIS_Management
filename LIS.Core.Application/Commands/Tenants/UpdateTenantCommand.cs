using LIS.Core.Application.ModelMetas;
using LIS.Infrastructure.Models;
using MediatR;
using System;

namespace LIS.Core.Application.Commands.Tenants
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
