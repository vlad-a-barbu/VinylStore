using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VinylStore.Web.Validation;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        var modelState = actionContext.ModelState;

        if (!modelState.IsValid)
        {
            actionContext.Result = new BadRequestObjectResult(modelState);
        }
    }
}
