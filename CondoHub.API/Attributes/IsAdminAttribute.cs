using Microsoft.AspNetCore.Mvc.Filters;

namespace CondoHub.API.Attributes
{
    public class IsAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
