using MediatR;
using Medlatec.Core.Application.Commands.Pages;
using Medlatec.Core.Application.ModelMetas;
using Medlatec.Core.Application.Queries.PageQuery;
using Medlatec.Infrastructure.Constants;
using Medlatec.Infrastructure.Controllers;
using Medlatec.Infrastructure.Filters;
using Medlatec.Infrastructure.Helpers;
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

        //[Route("{id}"), AcceptVerbs("GET")]
        //public async Task<IActionResult> Detail(int id)
        //{
        //    return Ok(await _pageService.Detail(id));
        //}

        [AcceptVerbs("POST"), ValidateModel]
        [AllowPermission(PageId.ConfigPage, Permission.Insert)]
        [CheckPermission]
        public async Task<IActionResult> Insert([FromBody] PageMeta pageMeta)
        {
            var result = await _mediator.Send(new InsertPageCommand(
                pageMeta.Id,
                pageMeta.Name,
                pageMeta.Description,
                CurrentUser.TenantId,
                pageMeta.IsActive,
                pageMeta.Icon,
                pageMeta.Order,
                pageMeta.ParentId,
                pageMeta.IsShowSidebar,
                pageMeta.Url));

            if (result.Code <= 0)
                return BadRequest(result);

            return Ok(result);
        }

        //[Route("{id}"), AcceptVerbs("POST")]
        //[AllowPermission(PageId.ConfigPage, Permission.Update)]
        //[CheckPermission]
        //public async Task<IActionResult> Update([FromBody] PageMeta pageMeta)
        //{
        //    var result = await _pageService.Update(pageMeta);
        //    if (result.Code <= 0)
        //        return BadRequest(result);

        //    return Ok(result);
        //}

        //[Route("{id}"), AcceptVerbs("DELETE")]
        //[AllowPermission(PageId.ConfigPage, Permission.Delete)]
        //[CheckPermission]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _pageService.Delete(id);
        //    if (result.Code <= 0)
        //        return BadRequest(result);

        //    return Ok(result);
        //}

        [Route("trees"), AcceptVerbs("GET")]
        [AllowPermission(PageId.ConfigPage, Permission.View)]
        [CheckPermission]
        public async Task<IActionResult> GetPageTree()
        {
            var result = await _mediator.Send(new GetFullPagesTreeQuery());
            return Ok(result);
        }
    }
}
