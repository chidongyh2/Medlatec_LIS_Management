using AspNet.Security.OpenIdConnect.Primitives;
using LIS.Authentication.Api.Resources;
using LIS.Authentication.Helpers;
using LIS.Authentication.Infrastructure;
using LIS.Authentication.Services;
using LIS.Infrastructure.Domain.AccountAggregate;
using LIS.Infrastructure.IServices;
using LIS.Infrastructure.Localization;
using LIS.Infrastructure.Resources;
using LIS.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json;

namespace LIS.Authentication.Api
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
            services.AddCustomDbContext(Configuration)
                     .AddIoC(Configuration);

            services.AddControllers();

            services.AddMvc().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            services.AddSwagger();
            services.ConfigureLocalization();

            services.AddIdentity<Account, Role>()
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
                    .AddDefaultTokenProviders();

            services.AddIdentityServer()
                   .AddDeveloperSigningCredential()
                   .AddAspNetIdentity<Account>()
                   .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                   .AddConfigurationStore(opts =>
                   {
                       opts.ConfigureDbContext = co => co.UseOracle(Configuration.GetConnectionString("AuthConnectionString"), occ =>
                       {
                           occ.MigrationsAssembly(_assemblyName);
                           occ.MigrationsHistoryTable("__EFMigrationsHistory");
                       });
                   })
                   .AddOperationalStore(opts =>
                   {
                       opts.ConfigureDbContext = co => co.UseOracle(Configuration.GetConnectionString("AuthConnectionString"), occ =>
                       {
                           occ.MigrationsAssembly(_assemblyName);
                           occ.MigrationsHistoryTable("__EFMigrationsHistory");
                       });
                       opts.EnableTokenCleanup = true;
                       opts.TokenCleanupInterval = 3600;
                   })
                   .AddProfileService<ProfileService>()
                   .AddJwtBearerClientAuthentication();

            services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequiredUniqueChars = 0;
                opts.Password.RequiredLength = 6;
                opts.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                opts.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                opts.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
                opts.SignIn.RequireConfirmedEmail = false;
                opts.User.RequireUniqueEmail = false;
            });

            // initialize lifecycle service
            services.AddScoped<IResourceService<SharedResource>, ResourceService<SharedResource>>();
            services.AddScoped<IResourceService<AuthResource>, ResourceService<AuthResource>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseExceptionHandler("/error");
            app.UseHttpsRedirection();
            app.UseCors(opts =>
            {
                opts.AllowAnyHeader();
                opts.AllowAnyMethod();
                opts.AllowCredentials();
                opts.SetIsOriginAllowed(origin => true);
            });

            app.UseSwaggerUi();
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax }); // 

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
