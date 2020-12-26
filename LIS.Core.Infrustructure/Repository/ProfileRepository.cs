using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LIS.Core.Domain.IRepository;
using LIS.Infrastructure.Helpers;
using LIS.Infrastructure.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using LIS.Infrastructure.Domain.AccountAggregate;
using System;

namespace LIS.Core.Infrastructure.Repository
{
    public class ProfileRepository : IProfileService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUserClaimsPrincipalFactory<Account> _claimsFactory;

        public ProfileRepository(IUserAccountRepository userAccountRepository, IUserClaimsPrincipalFactory<Account> claimsFactory)
        {
            _userAccountRepository = userAccountRepository;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //var sub = context.Subject.GetSubjectId();
            //var userInfo = await _userAccountRepository.GetInfo(sub, true);
            //var principal = await _claimsFactory.CreateAsync(userInfo);
            //var roles = await _userRoleRepository.GetsAllByUserId(sub);
            //var roleString = roles != null && roles.Any() ? roles.Select(x => x.RoleId).Join(",") : string.Empty;
            //var userInfoString = JsonConvert.SerializeObject(new BriefUser
            //{
            //    Id = userInfo.Id,
            //    FullName = userInfo.FullName,
            //    TenantId = userInfo.TenantId,
            //    Avatar = userInfo.Avatar,
            //    Email = userInfo.Email,
            //    PhoneNumber = userInfo.PhoneNumber,
            //    UserName = userInfo.UserName
            //});
            //var userInfoEncrypted = EncryptionHelper.Encrypt(userInfoString, userInfo.Id);
            //context.IssuedClaims.AddRange(principal.Claims);
            //context.IssuedClaims.Add(new Claim("ui", userInfoEncrypted));
            //context.IssuedClaims.Add(new Claim(ClaimTypes.Role, roleString));
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var userInfo = await _userAccountRepository.GetInfo(Guid.Parse(sub), true);
            context.IsActive = userInfo != null;
        }
    }
}
