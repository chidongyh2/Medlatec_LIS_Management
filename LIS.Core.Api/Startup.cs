using LIS.Core.Api.Helpers;
using LIS.Core.Infrastructure;
using LIS.Core.Infrustructure;
using LIS.Infrastructure.Localization;
using LIS.Infrastructure.Mvc;
using LIS.Infrastructure.SeedWorks;
using LIS.Infrastructure.UowInterceptor.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Reflection;
using System.Text.Json;

namespace LIS.Core.Api
{
    public class Startup
    {
        public static readonly string _assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<CoreDbContext>(options =>
                    options.UseLazyLoadingProxies()
                        .UseOracle(Configuration.GetConnectionString("CoreConnectionString"), ops =>
                        {
                            //ops.MigrationsAssembly(_assemblyName);
                            //ops.MigrationsHistoryTable("__EFMigrationsHistory");
                        }))
                .AddScoped<IDbConnection>(sp =>
                {
                    var config = sp.GetRequiredService<IConfiguration>();
                    var connection = new OracleConnection(config.GetConnectionString("CoreConnectionString"));
                    connection.Open();
                    return connection;
                })
                .AddUnitOfWorkInterceptors(AppDomain.CurrentDomain.GetAssemblies());

            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddIoC(Configuration)
                .AddControllers();

            services.AddMvc().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            services.AddSwagger();
            services.ConfigureLocalization();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, CoreUnitOfWork>();
            services.AddScoped<IScopeContext, ScopeContext>();
            services.AddMediatR(typeof(Application.Assembly).GetTypeInfo().Assembly);
            services.AddCustomAuthentication(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region start language support
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(opts =>
            {
                opts.AllowAnyHeader();
                opts.AllowAnyMethod();
                opts.AllowCredentials();
                opts.SetIsOriginAllowed(origin => true);
            });
            app.UseSwaggerUi();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
