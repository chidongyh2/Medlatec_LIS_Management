using Medlatec.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Medlatec.Core.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class AppsController : MedControllerBase
    {

    }
}
