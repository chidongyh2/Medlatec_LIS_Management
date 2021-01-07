using Medlatec.Infrastructure.Extensions;
using Medlatec.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Medlatec.Infrastructure.Mvc
{
    public class ScopeContext : IScopeContext
    {
        public BriefUser CurrentUser { get; private set; }
        public ScopeContext(IHttpContextAccessor contextAccessor)
        {
            CurrentUser = contextAccessor.HttpContext.GetCurrentUser();
        }
    }
}
