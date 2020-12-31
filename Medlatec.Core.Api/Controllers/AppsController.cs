using Medlatec.Core.Domain.IRepository;
using Medlatec.Infrastructure.Constants;
using Medlatec.Infrastructure.Controllers;
using Medlatec.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Medlatec.Core.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class AppsController : MedControllerBase
    {
        private readonly IPageRepository _pageRepository;
        public AppsController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        [AcceptVerbs("GET")]
        [AllowPermission(PageId.Config, Permission.View)]
        [CheckPermission]
        public async Task<IActionResult> InitApp()
        {
            var lstData = await _pageRepository.GetInfo(1);
            return Ok(lstData.Name);
        }
    }
}
