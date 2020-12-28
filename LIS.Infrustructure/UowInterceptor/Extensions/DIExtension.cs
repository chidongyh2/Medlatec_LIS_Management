using LIS.Infrastructure.UowInterceptor.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace LIS.Infrastructure.UowInterceptor.Extensions
{
    public static class DiExtension
    {
        public static void AddUnitOfWorkInterceptors(this IServiceCollection services, Assembly[] assemblies)
        {
            assemblies.Where(t => !t.FullName.Contains("Oracle.ManagedDataAccess")).SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.ImplementInterface(typeof(IOnBeforeCommit)))
                .ToList()
                .ForEach(t => { services.AddTransient(typeof(IOnBeforeCommit), t); });

            assemblies.Where(t => !t.FullName.Contains("Oracle.ManagedDataAccess")).SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.ImplementInterface(typeof(IOnAfterCommit)))
                .ToList()
                .ForEach(t => { services.AddTransient(typeof(IOnAfterCommit), t); });

            assemblies.Where(t => !t.FullName.Contains("Oracle.ManagedDataAccess")).SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.ImplementInterface(typeof(IOnAdd<>)))
                .ToList()
                .ForEach(t =>
                {
                    t.GetInterfaces().Where(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IOnAdd<>))
                        .ToList()
                        .ForEach(@interface =>
                        {
                            services.AddTransient(@interface, t);
                        });
                });

            assemblies.Where(t => !t.FullName.Contains("Oracle.ManagedDataAccess")).SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.ImplementInterface(typeof(IOnUpdate<>)))
                .ToList()
                .ForEach(t =>
                {
                    t.GetInterfaces().Where(i =>
                            i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IOnUpdate<>))
                        .ToList()
                        .ForEach(@interface =>
                        {
                            services.AddTransient(@interface, t);
                        });
                });

            assemblies.Where(t => !t.FullName.Contains("Oracle.ManagedDataAccess")).SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.ImplementInterface(typeof(IOnDelete<>)))
                .ToList()
                .ForEach(t =>
                {
                    t.GetInterfaces().Where(i =>
                            i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IOnDelete<>))
                        .ToList()
                        .ForEach(@interface =>
                        {
                            services.AddTransient(@interface, t);
                        });
                });

            services.AddScoped<IInterceptorManager, InterceptorManager>();
        }

        private static bool ImplementInterface(this Type targetType, Type @interface)
        {
            return @interface.IsGenericType
                ? targetType.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == @interface)
                : @interface.IsAssignableFrom(targetType);
        }
    }
}
