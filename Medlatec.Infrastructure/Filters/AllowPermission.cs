using Medlatec.Infrastructure.Constants;
using Medlatec.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Medlatec.Infrastructure.Filters
{
    public class AllowPermission : Attribute, IAsyncActionFilter
    {
        public PageId PageId { get; set; }
        public Permission[] Permissions { get; set; }
        public AllowPermission(PageId pageId, params Permission[] permissions)
        {
            PageId = pageId;
            Permissions = permissions;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var header = context.HttpContext.Request.Headers;
            header.TryGetValue("Permissions", out var permissionsString);
            var permissions = new List<PagePermission>();
            if (!string.IsNullOrEmpty(permissionsString))
            {
                permissions = JsonSerializer.Deserialize<List<PagePermission>>(permissionsString);
                permissions.Add(new PagePermission(PageId, Permissions));
            }
            else
            {
                permissions.Add(new PagePermission(PageId, Permissions));

            }
            if (header.ContainsKey("Permissions"))
            {
                header.Remove("Permissions");
                AddHeader();
            }
            else
            {
                AddHeader();
            }

            await next();

            void AddHeader()
            {
                header.Add(
                    new KeyValuePair<string, StringValues>("Permissions", JsonSerializer.Serialize(permissions)
                ));
            }
        }
    }
}
