using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using LIS.Infrastructure.InitializationStage;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LIS.Authentication.Initializations
{
    public class SeedClientInitialization : IInitializationStage
    {
        private readonly IConfiguration _configuration;
        private readonly ConfigurationDbContext _context;

        public SeedClientInitialization(IConfiguration configuration, ConfigurationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public int Order => 3;
        public async Task ExecuteAsync()
        {
            var clientUrls = _configuration.GetSection("IdentityServer:ClientUrls").Get<Dictionary<string, string[]>>();
            var apiSecrets = _configuration.GetSection("IdentityServer:ApiSecrets").Get<Dictionary<string, string[]>>();
            var clientSecrets = _configuration.GetSection("IdentityServer:ClientSecrets").Get<Dictionary<string, string>>();

            var allClients = _context.Clients.ToList();
            foreach (var client in IdentityConfiguration.GetClients(clientUrls, clientSecrets))
            {
                if (!allClients.Any(t => t.ClientId == client.ClientId))
                {
                    _context.Clients.Add(client.ToEntity());
                }

            }
            await _context.SaveChangesAsync();

            if (!_context.ApiScopes.Any())
            {
                foreach (var scope in IdentityConfiguration.ApiScopes)
                {
                    _context.ApiScopes.Add(scope.ToEntity());
                }
                await _context.SaveChangesAsync();
            }

            if (!_context.IdentityResources.Any())
            {
                foreach (var resource in IdentityConfiguration.IdentityResources)
                {
                    _context.IdentityResources.Add(resource.ToEntity());
                }
                await _context.SaveChangesAsync();
            }

            if (!_context.ApiResources.Any())
            {
                foreach (var api in IdentityConfiguration.GetApis(apiSecrets))
                {
                    _context.ApiResources.Add(api.ToEntity());
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
