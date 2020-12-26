using LIS.Core.Domain.IRepository;
using LIS.Infrastructure.Extensions;
using LIS.Infrastructure.Helpers;
using LIS.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using LIS.Infrastructure.Oracle;
using LIS.Infrastructure.Domain.AccountAggregate;

namespace LIS.Core.Infrastructure.Repository
{
    public class UserAccountRepository : RepositoryBase, IUserAccountRepository
    {
        private readonly IRepository<Account> _userAccountRepository;
        private readonly IConfiguration _configuration;
        public UserAccountRepository(IDbContext context, IConfiguration configuration) : base(context)
        {
            _userAccountRepository = Context.GetRepository<Account>();
            _configuration = configuration;
        }

        public async Task<int> Insert(Account userAccount)
        {
            _userAccountRepository.Create(userAccount);
            return await Context.SaveChangesAsync();
        }

        public async Task<string> GetUserIdAsync(Account user, CancellationToken cancellationToken)
        {
            var result = await _userAccountRepository.GetAsync(true, x => x.UserName == user.UserName);
            return result != null ? result.Id.ToString() : string.Empty;
        }

        public async Task<string> GetUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            var result = await _userAccountRepository.GetAsync(true, x => x.Id == user.Id);
            return result != null ? result.UserName : string.Empty;
        }

        public Task SetUserNameAsync(Account user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetNormalizedUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            var result = await _userAccountRepository.GetAsync(true, x => x.Id == user.Id || x.UserName == user.UserName);
            return result == null ? string.Empty : result.NormalizedUserName;
        }

        public Task SetNormalizedUserNameAsync(Account user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(Account user, CancellationToken cancellationToken)
        {
            // Check UserName Exists
            bool isUserNameExists = await CheckUserNameExists(user.Id, user.UserName);
            if (isUserNameExists)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "-1",
                    Description = "Tên đăng nhập đã tồn tại. Vui lòng kiểm tra lại."
                });
            }

            // Check Email Exists
            bool isEmailExists = await CheckEmailExists(user.Id, user.Email);
            if (isEmailExists)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "-2",
                    Description = "Email đã tồn tại. Vui lòng kiểm tra lại."
                });
            }

            user.NormalizedEmail = user.Email.ToUpperInvariant().StripVietnameseChars();
            user.NormalizedUserName = user.UserName.ToUpperInvariant().StripVietnameseChars();
            _userAccountRepository.Create(user);
            int result = await Context.SaveChangesAsync(cancellationToken);
            return result > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError
                {
                    Code = "0",
                    Description = "Có gì đó hoạt động chưa đúng. Vui lòng liên hệ với Quản Trị Viên."
                });
        }

        public async Task<IdentityResult> UpdateAsync(Account user, CancellationToken cancellationToken)
        {
            await Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Account user, CancellationToken cancellationToken)
        {
            var userInfo = await GetInfoByUserName(user.TenantId, user.UserName, true);
            if (userInfo == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "-1",
                    Description = "Thông tin người dùng cần xóa không tồn tại. Vui lòng kiểm tra lại."
                });
            }

            userInfo.SetDelete();
            int result = await Context.SaveChangesAsync(cancellationToken);
            return result <= 0
                ? IdentityResult.Failed(new IdentityError
                {
                    Code = "-2",
                    Description = "Xóa người dùng không thành công. Vui lòng liên hệ với Quản Trị Viên."
                })
                : IdentityResult.Success;
        }

        public async Task<Account> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _userAccountRepository.GetAsync(true, x => x.Id.ToString() == userId && !x.IsDelete);
        }

        public async Task<Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _userAccountRepository.GetAsync(true,
                x => x.NormalizedUserName == normalizedUserName && !x.IsDelete);
        }

        public Task SetPasswordHashAsync(Account user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPasswordHashAsync(Account user, CancellationToken cancellationToken)
        {
            Account result = await GetInfoByUserName(user.TenantId, user.UserName, true);
            return result == null ? string.Empty : user.PasswordHash;
        }

        public async Task<bool> HasPasswordAsync(Account user, CancellationToken cancellationToken)
        {
            return await _userAccountRepository.ExistAsync(
                x => x.Id == user.Id && !string.IsNullOrEmpty(x.PasswordHash));
        }

        public async Task<BriefUser> GetCurrentUser(Guid id)
        {
            return await _userAccountRepository.GetAsAsync(x => new BriefUser
            {
                Id = x.Id,
                UserName = x.UserName,
                ProfileImageUrl = x.ProfileImageUrl,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email
            }, x => x.Id == id && x.IsActive && !x.IsDelete && !x.LockoutEnd.HasValue);
        }

        public async Task<Account> GetInfo(Guid id, bool isReadOnly = false)
        {
            return await _userAccountRepository.GetAsync(isReadOnly, x => x.Id == id && !x.IsDelete && !x.LockoutEnd.HasValue);
        }

        public async Task<Account> GetInfoByUserName(Guid tenantId, string userName, bool isReadOnly = false)
        {
            return await _userAccountRepository.GetAsync(isReadOnly,
                x => x.NormalizedUserName.Equals(userName.ToUpper().Trim()) && x.IsActive && !x.IsDelete && x.TenantId == tenantId);
        }

        public async Task<Account> GetInfoByUserName(string userName, bool isReadOnly = false)
        {
            return await _userAccountRepository.GetAsync(isReadOnly,
                x => x.NormalizedUserName.Equals(userName.ToUpper().Trim()) && x.IsActive && !x.IsDelete);
        }

        public async Task<bool> CheckUserNameExists(Guid id, string userName)
        {
            return await _userAccountRepository.ExistAsync(x => x.Id != id && x.UserName == userName.Trim() && !x.IsDelete);
        }

        public async Task<bool> CheckEmailExists(Guid id, string email)
        {
            return await _userAccountRepository.ExistAsync(x => x.Id != id && x.Email == email.Trim() && !x.IsDelete);
        }

        public async Task<bool> CheckExistsByUserId(Guid userId)
        {
            return await _userAccountRepository.ExistAsync(x => x.Id == userId);
        }

        public int UpdateAccessFailCount(string userName, int failCount, bool lockoutOnFailure = false)
        {
            var info = Task.Run(() => GetInfoByUserName(userName)).Result;
            if (info == null)
            {
                return -1;
            }

            info.AccessFailedCount = failCount;
            if (lockoutOnFailure)
            {
                var defaultLockoutTimeSpan = _configuration.ConfigIdentity("DefaultLockoutTimeSpan") != null ? int.Parse(_configuration.ConfigIdentity("DefaultLockoutTimeSpan")) : 5;
                var maxFailedAccessAttempts = _configuration.ConfigIdentity("MaxFailedAccessAttempts") != null ? int.Parse(_configuration.ConfigIdentity("MaxFailedAccessAttempts")) : 5;

                info.LockoutEnd = failCount >= maxFailedAccessAttempts ? DateTime.Now.AddMinutes(defaultLockoutTimeSpan) : (DateTime?)null;
            }

            return Context.SaveChanges();
        }

        public async Task<int> UpdateUserAccount(Account userAccount)
        {
            return await Context.SaveChangesAsync();
        }

        public async Task<int> UpdatePassword(Guid userId, byte[] passwordSalt, string passwordHash)
        {
            return await Context.SaveChangesAsync();
        }

        public async Task<int> ResetLockout(string userName)
        {
            var info = await GetInfoByUserName(userName);
            if (info == null)
                return -1;

            info.LockoutEnd = null;
            info.AccessFailedCount = 0;
            return Context.SaveChanges();
        }

        public async Task<int> LockAccount(string userName)
        {
            var info = await GetAccountInfoByUserName(userName);
            if (info == null)
                return -1;

            var maxFailedAccessAttempts = _configuration.ConfigIdentity("MaxFailedAccessAttempts") != null ? int.Parse(_configuration.ConfigIdentity("MaxFailedAccessAttempts")) : 5;

            info.LockoutEnd = DateTime.MaxValue;
            info.AccessFailedCount = maxFailedAccessAttempts;
            return Context.SaveChanges();
        }

        public async Task<int> DeleteAccount(Account userAccount)
        {
            _userAccountRepository.Delete(userAccount);
            return await Context.SaveChangesAsync();
        }

        public async Task<Account> GetAccountInfoByUserName(string userName, bool isReadOnly = false)
        {
            return await _userAccountRepository.GetAsync(isReadOnly, x => x.NormalizedUserName.Equals(userName.ToUpper().Trim()) && !x.IsDelete);
        }

        public async Task<bool> ValidateCredentialsAsync(string userName, string password)
        {
            var userInfo = await GetInfoByUserName(userName);
            byte[] passwordHash = GenerateHelper.GetInputPasswordHash(password.Trim(), userInfo.PasswordSalt);
            return await _userAccountRepository.ExistAsync(x => x.UserName == userName && x.PasswordHash == Convert.ToBase64String(passwordHash));
        }
    }
}
