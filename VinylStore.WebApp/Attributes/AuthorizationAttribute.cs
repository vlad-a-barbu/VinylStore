using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VinylStore.DataAccess.EF.Models;
using VinylStore.DataObjects.AuthenticationModels;

namespace VinylStore.Web.Validation.Attributes;

public class AuthorizationAttribute : Attribute, IAuthorizationFilter
{
    private readonly ICollection<Role>? _roles;

    public AuthorizationAttribute(params Role[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (_roles is null || !IsAuthorized(context.HttpContext))
        {
            context.Result = new UnauthorizedObjectResult("Unauthorized user");
        }
    }
    
    private bool IsAuthorized(HttpContext context)
    {
        if (context.Items.TryGetValue("User", out var user))
        {
            return user is AuthenticatedUser authorizedUser
                   && _roles!.Contains(authorizedUser.Role);
        }

        return false;
    }
}