using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Medlatec.Infrastructure.Extensions
{
    public static class EntityFrameworkConfigExtension
    {
        public static IConfigurationBuilder AddEntityFrameworkConfig<T>(this IConfigurationBuilder builder, IDbConnection connection) where T : DbContext
        {
            return builder.Add(new EntityFrameworkConfigSourceExtension<T>(connection));
        }
    }
}
