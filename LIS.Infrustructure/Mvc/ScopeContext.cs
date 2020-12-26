using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LIS.Infrastructure.Mvc
{
    public class ScopeContext : IScopeContext
    {
        public string UserId { get; private set; }
        public string UserFullName { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public ScopeContext(IHttpContextAccessor contextAccessor)
        {
            var claims = contextAccessor.HttpContext?.User;
            UserId = claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            UserFullName = $"{claims?.FindFirst("given_name")?.Value} {claims?.FindFirst("family_name")?.Value}";
            Email = claims?.FindFirst("email")?.Value;
            UserName = claims?.FindFirst(ClaimTypes.WindowsAccountName)?.Value ?? string.Empty;
        }
    }
}
