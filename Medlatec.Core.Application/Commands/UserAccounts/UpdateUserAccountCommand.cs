using Medlatec.Core.Application.ModelMetas;
using System;

namespace Medlatec.Core.Application.Commands.UserAccounts
{
    public class UpdateUserAccountCommand
    {
        public Guid UserId { get; private set; }
        public AccountUserMeta AccountUserMeta { get; private set; }
        public UpdateUserAccountCommand(Guid userId, AccountUserMeta accountUserMeta)
        {
            UserId = userId;
            AccountUserMeta = accountUserMeta;
        }
    }
}
