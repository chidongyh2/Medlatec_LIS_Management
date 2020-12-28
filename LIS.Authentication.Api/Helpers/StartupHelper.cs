using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using LIS.Authentication.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Microsoft.AspNetCore.Http;
using LIS.Infrastructure.InitializationStage;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using LIS.Infrastructure.Constants;
using Microsoft.OpenApi.Models;
using LIS.Authentication.Initializations;

namespace LIS.Authentication.Helpers
{
    public static class StartupHelper
    {
        public static readonly string AssemblyName = typeof(StartupHelper).GetTypeInfo().Assembly.GetName().Name;

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<IdentityDbContext>(options =>
                    options.UseLazyLoadingProxies()
                        .UseOracle(configuration.GetConnectionString("AuthConnectionString"), b =>
                        {
                            b.MigrationsAssembly(AssemblyName);
                            b.MigrationsHistoryTable("__EFMigrationsHistory");
                        }))
                .AddScoped<IDbConnection>(sp =>
                    {
                        var config = sp.GetRequiredService<IConfiguration>();
                        var connection = new OracleConnection(config.GetConnectionString("AuthConnectionString"));
                        connection.Open();
                        return connection;
                    });
            return services;
        }

        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IInitializationStage, MigrateDatabaseInitialization>();
            services.AddTransient<IInitializationStage, SeedClientInitialization>();
            return services;
        }
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    var settings = configuration.GetSection("Authentication").Get<IdentitySettings>();
                    options.Authority = settings.Authority;
                    options.RequireHttpsMetadata = settings.RequireHttpsMetadata;
                    options.ApiName = settings.ApiName;
                    options.ApiSecret = settings.ApiSecret;
                    //options.TokenRetriever = CustomTokenRetriever.FromHeaderAndQueryString();
                    //options.EnableCaching = true;
                    //options.CacheDuration = TimeSpan.FromHours(1);
                });

            return services;
        }

        public static void UseSwaggerUi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LIS Authentication API v1");
                c.DocumentTitle = "LIS Authentication API";
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LIS Authentication API", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
                c.AddSecurityRequirement(securityRequirement);
            });
        }
    }
}
