using LIS.Infrastructure.Models;
using LIS.Infrastructure.ViewModel;
using MediatR;
using System;

namespace LIS.Core.Application.Queries.UserAccountQuery
{
    public class GetUserInfoQuery : IRequest<ActionResultResponse<UserInfoViewModel>>
    {
        public Guid Id { get; private set; }
        public GetUserInfoQuery(Guid id)
        {
            Id = id;
        }
    }
}
