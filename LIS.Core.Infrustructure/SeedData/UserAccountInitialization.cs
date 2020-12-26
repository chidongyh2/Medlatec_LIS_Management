using LIS.Core.Domain.IRepository;
using LIS.Core.Infrastructure;
using LIS.Infrastructure.Constants;
using LIS.Infrastructure.Domain.AccountAggregate;
using LIS.Infrastructure.Helpers;
using LIS.Infrastructure.InitializationStage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LIS.Core.Infrustructure.SeedData
{
    public class UserAccountInitialization : IInitializationStage
    {
        public int Order => 1;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly CoreDbContext _context;
        private readonly IRoleRepository _roleRepository;
        public UserAccountInitialization(CoreDbContext context, IRoleRepository roleRepository,
            IUserAccountRepository userAccountRepository)
        {
            _context = context;
            _userAccountRepository = userAccountRepository;
            _roleRepository = roleRepository;
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
                var superAdminRole = await _roleRepository.GetRoleByRoleName(AuthRole.SuperAdmin);
                userAccount.SetRole(superAdminRole.Id);
                _context.Set<Account>().Add(userAccount);
                _context.SaveChanges();
                #endregion
            }
        }
    }
}
