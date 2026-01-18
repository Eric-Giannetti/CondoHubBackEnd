using System.Security.Claims;
using CondoHub.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CondoHub.API.Attributes;

public class IsCondominiumManagerAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userTypeClaim = context.HttpContext.User.FindFirst(ClaimTypes.System)?.Value;

        if (string.IsNullOrEmpty(userTypeClaim) ||
            !Enum.TryParse<TypeUserEnum>(userTypeClaim, out var userType) ||
            userType != TypeUserEnum.CondominiumManager)
        {
            context.Result = new ForbidResult();
            return;
        }

        base.OnActionExecuting(context);
    }
}