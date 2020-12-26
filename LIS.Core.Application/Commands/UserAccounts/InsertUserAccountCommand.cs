using LIS.Core.Application.ModelMetas;
using LIS.Infrastructure.Models;
using MediatR;

namespace LIS.Core.Application.Commands.UserAccounts
{
    public class InsertUserAccountCommand : IRequest<ActionResultResponse>
    {
        public AccountUserMeta AccountUserMeta { get; private set; }
        public InsertUserAccountCommand(AccountUserMeta accountUserMeta)
        {
            AccountUserMeta = accountUserMeta;
        }
    }
}
