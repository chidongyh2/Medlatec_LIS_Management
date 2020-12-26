using LIS.Infrastructure.UowInterceptor.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LIS.Infrastructure.UowInterceptor
{
    public class InterceptorManager : IInterceptorManager
    {
        private readonly IServiceProvider _serviceProvider;

        public InterceptorManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Execute(IEnumerable<EntityEntry> changeSet)
        {
            foreach (var entityEntry in changeSet)
            {
                await Handle(entityEntry);
            }
        }

        public async Task BeforeCommit()
        {
            foreach (var handler in _serviceProvider.GetServices<IOnBeforeCommit>())
            {
                await handler.OnBeforeCommit();
            }
        }

        public async Task AfterCommit()
        {
            foreach (var handler in _serviceProvider.GetServices<IOnAfterCommit>())
            {
                await handler.OnAfterCommit();
            }
        }

        private async Task Handle(EntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    {
                        var handlerType = typeof(IOnAdd<>).MakeGenericType(entry.Metadata.ClrType);
                        var handlers = _serviceProvider.GetServices(handlerType);

                        foreach (var handler in handlers)
                        {
                            if (handlerType.GetMethod("OnAdd") != null)
                            {
                                await (Task)handlerType.GetMethod("OnAdd").Invoke(handler, new[] { entry.Entity });
                            }
                        }

                        break;
                    }
                case EntityState.Deleted:
                    {
                        var handlerType = typeof(IOnDelete<>).MakeGenericType(entry.Metadata.ClrType);
                        var handlers = _serviceProvider.GetServices(handlerType);
                        foreach (var handler in handlers)
                        {
                            if (handlerType.GetMethod("OnDelete") != null)
                            {
                                await (Task)handlerType.GetMethod("OnDelete").Invoke(handler, new[] { entry.Entity });
                            }
                        }

                        break;
                    }
                case EntityState.Modified:
                    {
                        var handlerType = typeof(IOnUpdate<>).MakeGenericType(entry.Metadata.ClrType);
                        var handlers = _serviceProvider.GetServices(handlerType);
                        foreach (var handler in handlers)
                        {
                            if (handlerType.GetMethod("OnUpdate") != null)
                            {
                                await (Task)handlerType.GetMethod("OnUpdate").Invoke(handler, new[] { entry.Entity });
                            }
                        }
                        break;
                    }
            }
        }
    }
}
