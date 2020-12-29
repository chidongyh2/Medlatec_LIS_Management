using LIS.Authentication.Infrastructure;
using LIS.Infrastructure.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Client;

namespace LIS.Authentication.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().StartInitializationProcess().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureAppConfiguration((context, config) => {
                        var currentConfig = config.Build();
                        var connection = new OracleConnection(currentConfig.GetConnectionString("AuthConnectionString"));
                        config.AddEntityFrameworkConfig<IdentityDbContext>(connection);
                    });
                });
    }
}
