using Microsoft.AspNetCore.Identity;
using System;

namespace Medlatec.Infrastructure.Domain.AccountAggregate
{
    public class UserConnection: IdentityUserLogin<Guid>
    {
        /// <summary>
        /// Connection id (Signalr connection id)
        /// </summary>
        public Guid? ConnectionId { get; set; }
    }
}
