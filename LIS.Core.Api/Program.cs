using LIS.Core.Infrastructure;
using LIS.Infrastructure.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Client;
using System.IO;

namespace LIS.Core.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().StartInitializationProcess().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webuilder =>
               {
                   webuilder.UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>()
                   .ConfigureAppConfiguration((hostingContext, config) =>
                   {
                       config
                           .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                           .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                           .AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json", true)
                           .AddEnvironmentVariables();

                       var currentConfig = config.Build();
                       var connectionString = currentConfig.GetConnectionString("CoreConnectionString");
                       var connection = new OracleConnection(currentConfig.GetConnectionString("CoreConnectionString"));
                       config.AddEntityFrameworkConfig<CoreDbContext>(connection);
                   });
               });
    }
}
