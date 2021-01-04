using MediatR;
using Medlatec.Core.Application.Queries.PageQuery;
using Medlatec.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Medlatec.Core.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PagesController : MedControllerBase
    {
        private readonly IMediator _mediator;
        public PagesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AcceptVerbs("GET")]
        public async Task<IActionResult> Search(string keyword, string sort, bool? isActive, int page = 1, int pageSize = 20)
        {
            var result = await _mediator.Send(new SearchPagesQuery(keyword, sort, page, pageSize, isActive));
            return Ok(result);
        }
    }
}
