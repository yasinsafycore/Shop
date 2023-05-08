using Microsoft.AspNetCore.Mvc.Filters;

namespace Shop.Web.ActionFilters
{
    public class RedirectActionFilters : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.HttpContext.Response.Redirect("/");
            }
        }
    }
}
