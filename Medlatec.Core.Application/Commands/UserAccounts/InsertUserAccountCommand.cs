using Medlatec.Core.Application.ModelMetas;
using Medlatec.Infrastructure.Models;
using MediatR;

namespace Medlatec.Core.Application.Commands.UserAccounts
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
