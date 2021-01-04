using Medlatec.Core.Application.ViewModels;
using MediatR;
using System;

namespace Medlatec.Core.Application.Queries
{
    public class GetAppSettingsQuery : IRequest<AppSettingViewModel>
    {
        public Guid UserId { get; private set; }
        public Guid TenantId { get; private set; }
        public GetAppSettingsQuery(Guid userId, Guid tenantId)
        {
            UserId = userId;
            TenantId = tenantId;
        }
    }
}
