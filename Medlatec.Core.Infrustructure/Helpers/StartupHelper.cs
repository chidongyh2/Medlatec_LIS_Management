using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using Medlatec.Infrastructure.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using Medlatec.Infrastructure.InitializationStage;
using Medlatec.Core.Infrustructure.Initializations;
using Medlatec.Core.Infrastructure.SeedData;
using FluentValidation;
using Medlatec.Core.Application.ModelMetas;
using Medlatec.Core.Application.Validations;

namespace Medlatec.Core.Api.Helpers
{
    public static class StartupHelper
    {
        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
        {
            // init migration, seed data
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IInitializationStage, MigrateDatabaseInitialization>();
            services.AddTransient<IInitializationStage, PageSeedInitialization>();
            services.AddTransient<IInitializationStage, SeedRoleInitialization>();
            services.AddTransient<IInitializationStage, SeedTenantInitialization>();
            return services;
        }

        public static IServiceCollection AddValidations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IValidator<PageMeta>, PageMetaValidator>();
            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    var settings = configuration.GetSection("Authentication").Get<IdentitySettings>();
                    options.Authority = settings.Authority;
                    options.RequireHttpsMetadata = false;
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LIS Core Service API v1");
                c.DocumentTitle = "LIS Service API";
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LIS Core Service API", Version = "v1" });

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
