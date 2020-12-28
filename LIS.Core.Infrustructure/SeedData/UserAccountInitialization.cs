using LIS.Core.Domain.IRepository;
using LIS.Core.Infrastructure;
using LIS.Infrastructure.Constants;
using LIS.Infrastructure.Domain.AccountAggregate;
using LIS.Infrastructure.Helpers;
using LIS.Infrastructure.InitializationStage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LIS.Core.Infrastructure.SeedData
{
    public class UserAccountInitialization : IInitializationStage
    {
        public int Order => 5;
        private readonly CoreDbContext _context;
        public UserAccountInitialization(CoreDbContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync()
        {
            if (!_context.Set<Account>().Any())
            {
                #region UserAccount.
                var passwordSalt = GenerateHelper.GenerateRandomBytes(GenerateHelper.PasswordSaltLength);
                var passwordHash = GenerateHelper.GetInputPasswordHash("123456", passwordSalt);
                var userAccount = new Account("ngoquy97it@gmail.com", "Ngô Văn",
                    "Quý", null, null, null, true, Convert.ToBase64String(passwordHash), passwordSalt);
                var role = _context.Set<Role>().ToList();
                var superAdminRole = _context.Set<Role>().Where(x => x.Name == AuthRole.SuperAdmin).FirstOrDefault();
                userAccount.SetRole(superAdminRole.Id);
                _context.Set<Account>().Add(userAccount);
                _context.SaveChanges();
                #endregion
            }
        }
    }
}
