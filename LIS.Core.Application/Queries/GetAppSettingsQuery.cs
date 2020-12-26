using LIS.Core.Application.ViewModels;
using MediatR;
using System;

namespace LIS.Core.Application.Queries
{
    public class GetAppSettingsQuery : IRequest<AppSettingViewModel>
    {
        public Guid UserId { get; private set; }
        public GetAppSettingsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
