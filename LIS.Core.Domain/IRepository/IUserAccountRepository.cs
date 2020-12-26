using System;
using System.Threading.Tasks;
using LIS.Infrastructure.Domain.AccountAggregate;
using LIS.Infrastructure.Models;
using LIS.Infrastructure.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace LIS.Core.Domain.IRepository
{
    public interface IUserAccountRepository : IUserPasswordStore<Account>
    {
        Task<int> Insert(Account userAccount);
        Task<BriefUser> GetCurrentUser(Guid id);

        Task<Account> GetInfo(Guid id, bool isReadOnly = false);

        /// <summary>
        /// Lấy thông tin tài khoản by username không check isActive
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        Task<Account> GetAccountInfoByUserName(string userName, bool isReadOnly = false);

        Task<Account> GetInfoByUserName(Guid tenantId, string userName, bool isReadOnly = false);
        /// <summary>
        /// Lấy thông tin tài khoản có check isActive
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        Task<Account> GetInfoByUserName(string userName, bool isReadOnly = false);

        Task<bool> CheckUserNameExists(Guid id, string userName);

        Task<bool> ValidateCredentialsAsync(string userName, string password);

        Task<bool> CheckEmailExists(Guid id, string email);

        Task<bool> CheckExistsByUserId(Guid userId);

        int UpdateAccessFailCount(string userName, int failCount, bool lockoutOnFailure = false);

        Task<int> ResetLockout(string userName);

        Task<int> LockAccount(string userName);

        Task<int> UpdateUserAccount(Account userAccount);

        Task<int> UpdatePassword(Guid userId, byte[] passwordSalt, string passwordHash);

        Task<int> DeleteAccount(Account userAccount);
    }
}
