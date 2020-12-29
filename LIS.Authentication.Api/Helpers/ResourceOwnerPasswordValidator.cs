using IdentityModel;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using LIS.Infrastructure.Domain.AccountAggregate;
using LIS.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LIS.Authentication.Helpers
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly IEventService _events;
        private readonly ILogger<ResourceOwnerPasswordValidator<Account>> _logger;

        public ResourceOwnerPasswordValidator(UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            IEventService events,
            ILogger<ResourceOwnerPasswordValidator<Account>> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _events = events;
            _logger = logger;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(context.UserName.ToUpper());
                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        _logger.LogInformation("Authentication failed for username: {username}, reason: inactive", context.UserName);
                        await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "inactive", interactive: false));
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "account_not_active");
                        return;
                    }
                    var result = await _signInManager.CheckPasswordSignInAsync(user, context.Password, true);
                    if (result.Succeeded)
                    {
                        var sub = await _userManager.GetUserIdAsync(user);
                        _logger.LogInformation("Credentials validated for username: {username}", context.UserName);
                        await _events.RaiseAsync(new UserLoginSuccessEvent(context.UserName, sub, context.UserName, interactive: false));
                        context.Result = new GrantValidationResult(sub, OidcConstants.AuthenticationMethods.Password);
                        return;
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogInformation("Authentication failed for username: {username}, reason: locked out", context.UserName);
                        await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "locked out", interactive: false));
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, user.LockoutEnd.HasValue ? Math.Round(user.LockoutEnd.Value.Subtract(DateTimeOffset.UtcNow).TotalSeconds).ToString() : null, new Dictionary<string, object>() { { "locked", true } });
                        return;
                    }

                    if (result.IsNotAllowed)
                    {
                        _logger.LogInformation("Authentication failed for username: {username}, reason: not allowed", context.UserName);
                        await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "not allowed", interactive: false));
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "account_has_been_locked");
                        return;
                    }

                    _logger.LogInformation("Authentication failed for username: {username}, reason: invalid credentials", context.UserName);
                    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "invalid credentials", interactive: false));
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid_username_or_password");
                    return;
                }
                _logger.LogInformation("No user found matching username: {username}", context.UserName);
                await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "invalid username", interactive: false));
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid_username_or_password");
            } catch(Exception e)
            {
                throw e;
            }
        }
    }
}
