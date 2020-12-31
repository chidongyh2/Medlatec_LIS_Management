using MediatR;
using Medlatec.Core.Application.Queries.RoleQuery;
using Medlatec.Infrastructure.Controllers;
using Medlatec.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medlatec.Core.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/user-role")]
    public class UserRoleController : MedControllerBase
    {
        private readonly IMediator _mediator;
        public UserRoleController(IMediator mediator)
        {
            _mediator = mediator;
        } 
        [AcceptVerbs("POST")]
        [Route("{userId}")]
        public async Task<IActionResult> CheckPermission(Guid userId, [FromBody] IList<PagePermission> pagePermissions)
        {
            return Ok(await _mediator.Send(new CheckPermissionQuery(userId, pagePermissions)));
        }

    }
}
