using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LIS.Infrastructure.InitializationStage;
using LIS.Infrastructure.Constants;
using LIS.Infrastructure.Domain.AccountAggregate;

namespace LIS.Authentication.Initializations
{
    public class SeedRoleInitialization : IInitializationStage
    {
        private readonly RoleManager<Role> _roleManager;

        public SeedRoleInitialization(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public int Order => 4;
        public async Task ExecuteAsync()
        {
            var currentRoles = await _roleManager.Roles.ToListAsync();
            var allRoles = new List<Role>
            {
                new Role(AuthRole.SuperAdmin, AuthRole.SuperAdmin, AuthRole.SuperAdmin)
            };
            var newRoles = allRoles.Except(currentRoles).ToList();
            foreach (var item in newRoles)
            {
                await _roleManager.CreateAsync(item);
            }
        }
    }
}
