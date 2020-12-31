using IdentityModel.Client;
using Medlatec.Infrastructure.Extensions;
using Medlatec.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Medlatec.Infrastructure.Filters
{
    public class CheckPermission : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Request.Headers.TryGetValue("Permissions", out var permissionStringValues);
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorization);
            if (!string.IsNullOrEmpty(permissionStringValues))
            {
                var permissions = JsonConvert.DeserializeObject<List<PagePermission>>(permissionStringValues);
                if (permissions == null || !permissions.Any())
                {
                    context.Result = new ForbidResult();
                    return;
                }

                var configuration = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
                var apiUrl = configuration.GetApiUrl();
                if (apiUrl == null)
                {
                    context.Result = new BadRequestObjectResult(context.ModelState)
                    {
                        Value = new ActionResultResponse(-1,
                            "Missing some configuration. Please contact with Administrator.")
                    };
                    return;
                }

                if (string.IsNullOrEmpty(apiUrl.CheckPermissionUrl))
                {
                    context.Result = new BadRequestObjectResult(context.ModelState)
                    {
                        Value = new ActionResultResponse(-2,
                            "Authen fail. Please contact with Administrator.")
                    };
                    return;
                }
                var accessToken = authorization.ToString().Split(" ")[1];
                // call api for check permission
                var client = new HttpClient();
                client.SetBearerToken(accessToken);

                var userId = context.HttpContext.GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    context.Result = new ForbidResult();
                    return;
                }

                var content = JsonConvert.SerializeObject(permissions);
                var buffer = Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response =
                    await client.PostAsync($"{apiUrl.CheckPermissionUrl}{userId}", byteContent);
                if (!response.IsSuccessStatusCode)
                {
                    context.Result = new ForbidResult();
                    return;
                }

                var isHasPermission = bool.Parse(await response.Content.ReadAsStringAsync());
                if (!isHasPermission)
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
            await next();
        }
    }
}
