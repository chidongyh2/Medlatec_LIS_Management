using AspNet.Security.OpenIdConnect.Primitives;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LIS.Infrastructure.Constants;
using LIS.Infrastructure.Domain.AccountAggregate;

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
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;

            var user = await _userManager.FindByIdAsync(subjectId);
            if (user == null)
                throw new ArgumentException("Invalid subject identifier");
            var principal = await _claimsFactory.CreateAsync(user);
            var reqClaimTypes = context.RequestedClaimTypes.Concat(new[]
           {
                OpenIdConnectConstants.Claims.Role,
            });

            var claims = principal.Claims.Where(c => reqClaimTypes.Contains(c.Type)).ToList();
            claims.AddRange(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(OpenIdConnectConstants.Claims.Username, $"{user.LastName ?? string.Empty} {user.FirstName ?? string.Empty}"),
                new Claim(JwtClaimTypes.FamilyName, user.LastName ?? string.Empty),
                new Claim(JwtClaimTypes.GivenName, user.FirstName ?? string.Empty),
                new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber ?? string.Empty),
                new Claim(JwtClaimTypes.Picture, user.ProfileImageUrl ?? string.Empty),
                new Claim(JwtClaimTypes.Address, user.Address ?? string.Empty),
                new Claim("provider", user.UserConnections.Count > 0 ? user.UserConnections.FirstOrDefault().LoginProvider : string.Empty),
            });

            if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(new Claim(JwtClaimTypes.Email, user.Email));
            }
            if (user.ProfileImageUrl != null)
            {
                var profileImageUrl = user.ProfileImageUrl;
                claims.Add(new Claim(JwtClaimTypes.Picture, profileImageUrl));
            }
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null;
        }
    }
}
