using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIS.Infrastructure.SeedWorks
{
    public interface IChangeTrackingEntity
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }
        void ClearDomainEvents();
        void AddDomainEvent(INotification eventItem);
    }
}
