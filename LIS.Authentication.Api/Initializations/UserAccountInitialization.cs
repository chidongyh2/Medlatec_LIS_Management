using LIS.Authentication.Infrastructure;
using LIS.Infrastructure.Constants;
using LIS.Infrastructure.Domain.AccountAggregate;
using LIS.Infrastructure.InitializationStage;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace LIS.Authentication.Initializations
{
    public class UserAccountInitialization : IInitializationStage
    {
        public int Order => 5;
        private readonly IdentityDbContext _context;
        private readonly UserManager<Account> _userManager;
        public UserAccountInitialization(IdentityDbContext context, UserManager<Account> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task ExecuteAsync()
        {
            if (!_context.Set<Account>().Any())
            {
                #region UserAccount.
               
                var userAccount = new Account("ngoquy97it@gmail.com", "Ngô Văn",
                    "Quý", null, null, null, true, "123456");
                await _userManager.CreateAsync(userAccount, "123456");
                var role = _context.Set<Role>().ToList();
                var superAdminRole = _context.Set<Role>().Where(x => x.Name == AuthRole.SuperAdmin).FirstOrDefault();
                userAccount.SetRole(superAdminRole.Id);
                _context.SaveChanges();
                #endregion
            }
        }
    }
}
