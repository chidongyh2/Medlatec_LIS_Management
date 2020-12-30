using Medlatec.Core.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace Medlatec.Core.Application.Queries.UserAccountQuery
{
    public class GetsRolesByRoleIdQuery : IRequest<IList<UserRoleSearchViewModel>>
    {
        public Guid RoleId { get; private set; }
        public GetsRolesByRoleIdQuery(Guid roleId)
        {
            RoleId = roleId;
        }
    }
}
