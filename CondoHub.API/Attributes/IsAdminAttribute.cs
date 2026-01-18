using System.Security.Claims;
using CondoHub.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CondoHub.API.Attributes
{
    public class IsAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userTypeClaim = context.HttpContext.User.FindFirst(ClaimTypes.System)?.Value;

            if (string.IsNullOrEmpty(userTypeClaim) ||
                !Enum.TryParse<TypeUserEnum>(userTypeClaim, out var userType) ||
                userType != TypeUserEnum.Admin)
            {
                context.Result = new ForbidResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
