using Medlatec.Infrastructure.SeedWorks;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Medlatec.Infrastructure.Oracle;

namespace Medlatec.Infrastructure.Extensions
{
    public static class MediatrExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, IDbContext ctx, CancellationToken cancellationToken = default(CancellationToken))
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<IChangeTrackingEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());


            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
