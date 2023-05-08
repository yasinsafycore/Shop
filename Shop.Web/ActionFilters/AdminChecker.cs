using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Application.Extensions;
using Shop.Application.Services.Interfaces;

namespace Shop.Web.ActionFilters
{
    public class AdminChecker : Attribute, IAsyncAuthorizationFilter
    {
        private readonly int _permissionId;

        public AdminChecker(int permissionId)
        {
            _permissionId = permissionId;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService))!;

            if (!await userService.CheckUserPermission(_permissionId, context.HttpContext.User.GetUserId()))
            {
                context.Result = new RedirectResult("/");
            }
        }
    }
}
