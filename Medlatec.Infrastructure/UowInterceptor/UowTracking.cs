using Medlatec.Core.Infrastructure;
using Medlatec.Infrastructure.Mvc;
using Medlatec.Infrastructure.SeedWorks;
using Medlatec.Infrastructure.UowInterceptor.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Medlatec.Infrastructure.Extensions;
using Medlatec.Infrastructure.Oracle;

namespace Medlatec.Core.Infrustructure
{
    public class UowTracking : IOnBeforeCommit
    {
        private readonly IDbContext _dbContext;
        private readonly IScopeContext _scopeContext;
        private readonly IMediator _mediator;

        public UowTracking(IDbContext dbContext, IScopeContext scopeContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _scopeContext = scopeContext;
            _mediator = mediator;
        }
        public async Task OnBeforeCommit()
        {
            // await _mediator.DispatchDomainEventsAsync(_dbContext);

            var entityEntries = _dbContext.ChangeTracker.Entries().Where(x =>
                x.Entity is IEntity && (x.State == EntityState.Added || x.State == EntityState.Modified)).ToList();
            foreach (var entry in entityEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        {
                            if (entry.Entity is IModifierTrackingEntity)
                            {
                                entry.Property(nameof(IModifierTrackingEntity.CreatedById)).IsModified = true;
                                entry.Property(nameof(IModifierTrackingEntity.CreatedById)).CurrentValue = _scopeContext.CurrentUser.Id;
                                entry.Property(nameof(IModifierTrackingEntity.CreatedBy)).IsModified = true;
                                entry.Property(nameof(IModifierTrackingEntity.CreatedBy)).CurrentValue = _scopeContext.CurrentUser.FullName;
                            }
                            if (entry.Entity is IDateTracking)
                            {
                                entry.Property(nameof(IDateTracking.CreatedDate)).IsModified = true;
                                entry.Property(nameof(IDateTracking.CreatedDate)).CurrentValue = DateTimeOffset.Now;
                                entry.Property(nameof(IDateTracking.LastUpdatedDate)).IsModified = true;
                                entry.Property(nameof(IDateTracking.LastUpdatedDate)).CurrentValue = DateTimeOffset.Now;
                            }
                            break;
                        }

                    case EntityState.Modified:
                        {
                            if (entry.Entity is ModifierTrackingEntity)
                            {
                                entry.Property(nameof(IModifierTrackingEntity.LastUpdatedById)).IsModified = true;
                                entry.Property(nameof(IModifierTrackingEntity.LastUpdatedById)).CurrentValue = _scopeContext.CurrentUser.Id;
                                entry.Property(nameof(IModifierTrackingEntity.LastUpdatedBy)).IsModified = true;
                                entry.Property(nameof(IModifierTrackingEntity.LastUpdatedBy)).CurrentValue = _scopeContext.CurrentUser.FullName;
                            }
                            if (entry.Entity is IDateTracking)
                            {
                                entry.Property(nameof(IDateTracking.CreatedDate)).IsModified = false;
                                entry.Property(nameof(IDateTracking.LastUpdatedDate)).IsModified = true;
                                entry.Property(nameof(IDateTracking.LastUpdatedDate)).CurrentValue = DateTimeOffset.Now;
                            }
                            break;
                        }

                    case EntityState.Unchanged:
                    case EntityState.Detached:
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
