using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LIS.Infrastructure.Domain.AccountAggregate;
using LIS.Infrastructure.Helpers;
using Newtonsoft.Json;
using LIS.Infrastructure.Models;

namespace LIS.Authentication.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<Account> _userManager;
        private readonly IUserClaimsPrincipalFactory<Account> _claimsFactory;

        public ProfileService(UserManager<Account> userManager, IUserClaimsPrincipalFactory<Account> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

                var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;

                var user = await _userManager.FindByIdAsync(subjectId);
                if (user == null)
                    throw new ArgumentException("Invalid subject identifier");

                var principal = await _claimsFactory.CreateAsync(user);
                var userInfoString = JsonConvert.SerializeObject(new BriefUser
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    TenantId = user.TenantId,
                    ProfileImageUrl = user.ProfileImageUrl,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName
                });
                var userInfoEncrypted = EncryptionHelper.Encrypt(userInfoString, user.Id.ToString());
                context.IssuedClaims.AddRange(principal.Claims);
                context.IssuedClaims.Add(new Claim("ui", userInfoEncrypted));
            } catch(Exception e)
            {
                throw e;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null;
        }
    }
}
