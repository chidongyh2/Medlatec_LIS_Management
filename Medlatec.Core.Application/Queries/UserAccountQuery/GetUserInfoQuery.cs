using Medlatec.Infrastructure.Models;
using Medlatec.Infrastructure.ViewModel;
using MediatR;
using System;

namespace Medlatec.Core.Application.Queries.UserAccountQuery
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
