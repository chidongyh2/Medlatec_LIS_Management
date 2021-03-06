﻿using MediatR;
using Medlatec.Core.Application.Queries;
using Medlatec.Infrastructure.Controllers;
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
        private readonly IMediator _mediator;
        public AppsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AcceptVerbs("GET")]
        public async Task<IActionResult> InitApp()
        {
            var appSettings = await _mediator.Send(new GetAppSettingsQuery(CurrentUser.Id, CurrentUser.TenantId));
            appSettings.CurrentUser = CurrentUser;
            return Ok(appSettings);
        }
    }
}
