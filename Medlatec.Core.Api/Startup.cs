using Medlatec.Core.Api.Helpers;
using Medlatec.Core.Domain.IRepository;
using Medlatec.Core.Domain.Resources;
using Medlatec.Core.Infrastructure;
using Medlatec.Core.Infrastructure.Repository;
using Medlatec.Infrastructure.IServices;
using Medlatec.Infrastructure.Localization;
using Medlatec.Infrastructure.Mvc;
using Medlatec.Infrastructure.Oracle;
using Medlatec.Infrastructure.Resources;
using Medlatec.Infrastructure.SeedWorks;
using Medlatec.Infrastructure.Services;
using Medlatec.Infrastructure.UowInterceptor.Extensions;
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
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Medlatec.Infrastructure.Helpers;

namespace Medlatec.Core.Api
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
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services
                .AddDbContext<CoreDbContext>(options =>
                    options
                        .UseOracle(Configuration.GetConnectionString("CoreConnectionString"), ops =>
                        {
                            ops.MigrationsAssembly("Medlatec.Core.Infrastructure");
                            ops.MigrationsHistoryTable("__EFMigrationsHistory");
                        }))
                .AddScoped<IDbConnection>(sp =>
                {
                    var config = sp.GetRequiredService<IConfiguration>();
                    var connection = new OracleConnection(config.GetConnectionString("CoreConnectionString"));
                    connection.Open();
                    return connection;
                })
                .AddUnitOfWorkInterceptors(AppDomain.CurrentDomain.GetAssemblies());
            // fix Dapper GUID
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));
            SqlMapper.AddTypeHandler(typeof(Guid), new GuidTypeHandler());

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
            // Add resources support.            
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDbContext, CoreDbContext>();
            services.AddScoped<IUnitOfWork, CoreUnitOfWork>();
            services.AddScoped<IScopeContext, ScopeContext>();
            //init lifetime service DI
            services.AddScoped<IResourceService<SharedResource>, ResourceService<SharedResource>>();
            services.AddScoped<IResourceService<CoreResource>, ResourceService<CoreResource>>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IEthnicRepository, EthnicRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IReligionRepository, ReligionRepository>();
            services.AddScoped<IRolePageRepository, RolePageRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITenantPageRepository, TenantPageRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.AddMediatR(typeof(Application.Assembly).GetTypeInfo().Assembly)
                    .AddMediatR(typeof(Application.Write.Assembly).GetTypeInfo().Assembly)
                    .AddMediatR(typeof(Application.Read.Assembly).GetTypeInfo().Assembly);
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
