using LIS.Core.Application.ModelMetas;
using LIS.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIS.Core.Application.Commands.Tenants
{
    public class InsertTenantCommand: IRequest<ActionResultResponse>
    {
        public TenantMeta TenantMeta { get; private set; }
        public InsertTenantCommand(TenantMeta tenantMeta)
        {
            TenantMeta = tenantMeta;
        }
    }
}
