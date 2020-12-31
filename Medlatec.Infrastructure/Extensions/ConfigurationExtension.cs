using Medlatec.Infrastructure.Models;
using Microsoft.Extensions.Configuration;

namespace Medlatec.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static string ConfigIdentity(this IConfiguration configuration, string name)
        {
            var section = configuration?.GetSection("ConfigIdentity");
            return section?[name];
        }
        public static ApiUrlSettings GetApiUrl(this IConfiguration configuration)
        {
            var apiUrlSetting = new ApiUrlSettings();
            configuration?.GetSection("ApiUrlSettings")?.Bind(apiUrlSetting);
            return apiUrlSetting;
        }
    }
}
