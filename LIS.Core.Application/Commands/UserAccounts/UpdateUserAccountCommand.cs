using LIS.Core.Application.ModelMetas;
using System;

namespace LIS.Core.Application.Commands.UserAccounts
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
