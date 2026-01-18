using CondoHub.Domain.Services;
using CondoHub.Services.Context;
using System.Security.Claims;
using CondoHub.Domain.Enum;

namespace CondoHub.API.Services;

public class UserContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public UserContextMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context, IUserContextService userContextService)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userDataClaim = context.User.FindFirst(ClaimTypes.UserData)?.Value;
            if (!string.IsNullOrEmpty(userDataClaim) && DateTime.TryParse(userDataClaim, out var creationDate))
            {
                var expirationInDays = _configuration.GetValue<int>("Jwt:ExpirationInDays");
                if (DateTime.UtcNow > creationDate.AddDays(expirationInDays))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }
        }

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
        
        var typeUserEnumClaim = context.User?.FindFirst(ClaimTypes.System)?.Value;
        if (Enum.TryParse<TypeUserEnum>(typeUserEnumClaim, out var typeUserEnum))
        {
            userContextService.userEnum = typeUserEnum;
        }

        await _next(context);
    }
}
