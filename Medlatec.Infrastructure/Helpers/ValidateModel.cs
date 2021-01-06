using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Medlatec.Infrastructure.Helpers
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Result = new BadRequestObjectResult(actionContext.ModelState);
            }
        }
    }

    public class ModelStateFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Result = new BadRequestObjectResult(actionContext.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
