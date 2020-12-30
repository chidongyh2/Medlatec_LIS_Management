using Medlatec.Infrastructure.Extensions;
using Medlatec.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Medlatec.Infrastructure.Controllers
{
    public class MedControllerBase : ControllerBase
    {
        public BriefUser CurrentUser
        {
            get
            {
                var currentUser = HttpContext.GetCurrentUser();
                if (currentUser != null)
                {
                    return currentUser;
                }
                return null;
            }
        }
    }
}
