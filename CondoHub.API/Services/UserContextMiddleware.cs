using CondoHub.Domain.Services;
using CondoHub.Services.Context;
using System.Security.Claims;

namespace CondoHub.API.Services;

public class UserContextMiddleware
{
    private readonly RequestDelegate _next;

    public UserContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserContextService userContextService)
    {
        var userIdClaim = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (long.TryParse(userIdClaim, out var userId))
        {
            userContextService.UserId = userId;
        }

        var IsAdminClaim = context.User?.FindFirst(ClaimTypes.Actor)?.Value;
        if (bool.TryParse(IsAdminClaim, out var isAdmin))
        {
            userContextService.IsAdmin = isAdmin;
        }

        await _next(context);
    }
}
