using MediatR;
using System;

namespace Medlatec.Core.Application.Queries.UserAccountQuery
{
    public class CheckPermissonQuery : IRequest<bool>
    {
        public Guid UserId { get; private set; }
        public int PageId { get; private set; }
        public int[] Permissions { get; private set; }
        public CheckPermissonQuery(Guid userId, int pageId, int[] permissions)
        {
            UserId = userId;
            PageId = pageId;
            Permissions = permissions;
        }
    }
}
