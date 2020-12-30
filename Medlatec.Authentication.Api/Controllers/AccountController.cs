using Medlatec.Authentication.Api.Resources;
using Medlatec.Authentication.Requests;
using Medlatec.Infrastructure.Domain.AccountAggregate;
using Medlatec.Infrastructure.IServices;
using Medlatec.Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Medlatec.Authentication.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IResourceService<AuthResource> _resourceService;

        public AccountController(UserManager<Account> userManager, ILogger<AccountController> logger, IResourceService<AuthResource> resourceService)
        {
            _userManager = userManager;
            _logger = logger;
            _resourceService = resourceService;
        }

        [AllowAnonymous]
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            //var user = await _userManager.FindByNameAsync(request.Email);

            //if (user == null || user.Logins.Any())
            //{
            //    _logger.LogWarning(_localizer["No user found with this email"]);
            //    throw new BusinessRuleException(BusinessRule.EmailNotFound);
            //}
            //var roleNames = await _userManager.GetRolesAsync(user);
            //if (!IsClientAllowed(roleNames, request.ClientId))
            //{
            //    _logger.LogWarning(_localizer["No user found with this email"]);
            //    throw new BusinessRuleException(BusinessRule.EmailNotFound);
            //}
            //if (!user.IsActive)
            //{
            //    _logger.LogWarning(_localizer["User account is not active"]);
            //    throw new BusinessRuleException(BusinessRule.UserAccountNotActive);
            //}
            //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);
            if (user == null)
            {
                _logger.LogWarning(_resourceService.GetString("No user found with this email"));
                return BadRequest(new ActionResultResponse(-1, _resourceService.GetString("No user found with this email")));
            }
            if (!user.IsActive)
            {
                _logger.LogWarning(_resourceService.GetString("User account is not active"));
                return BadRequest(new ActionResultResponse(-1, "User account is not active"));
            }
            var result = await _userManager.ResetPasswordAsync(user, request.Code, request.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            else return BadRequest(new ActionResultResponse(-1, "Reset password was error"));
        }
    }
}
