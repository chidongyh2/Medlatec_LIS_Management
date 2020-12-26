using Microsoft.Extensions.Configuration;

namespace LIS.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static string ConfigIdentity(this IConfiguration configuration, string name)
        {
            var section = configuration?.GetSection("ConfigIdentity");
            return section?[name];
        }
    }
}
