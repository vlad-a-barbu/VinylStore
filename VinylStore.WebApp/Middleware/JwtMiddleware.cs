using AutoMapper;
using Microsoft.Extensions.Options;
using VinylStore.DataAccess.EF.Models;
using VinylStore.DataAccess.Repositories;
using VinylStore.DataObjects.AuthenticationModels;
using VinylStore.Web.Authorization;

namespace VinylStore.Web.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtSecret _secret;
    private readonly IMapper _mapper;

    public JwtMiddleware(
        RequestDelegate next,
        IOptions<JwtSecret> secret,
        IMapper mapper)
    {
        _next = next;
        _secret = secret.Value;
        _mapper = mapper;
    }

    public async Task Invoke(
        HttpContext context,
        IGenericRepository<User> repository,
        JwtUtils utils
    )
    {
        var token = context
            .Request
            .Headers["Authorization"]
            .FirstOrDefault()?
            .Split(" ")
            .Last();
        
        var userId = utils.ValidateJwtToken(token);

        if (userId.HasValue)
        {
            var user = repository.Get(userId.Value)
                       ?? throw new Exception("Invalid token. User not found.");
            
            context.Items["User"] = _mapper.Map<AuthenticatedUser>(user);
        }

        await _next(context);
    }
}

public static class JwtMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtAuthorization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtMiddleware>();
    }
}
