using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using GWMBackend.Core.Helpers;
using GWMBackend.Core.Model.Base;
using GWMBackend.Service.Base;
using System.Security.Claims;
using System.Text;

namespace GWMBackend.Api.Utilities
{
    public class MyAthurizeFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        private IServiceWrapper _UserService;
        private string _pageName;
        private string _activity;
        //public MyAthurizeFilter(string pageName, string activity)
        //{
        //    _pageName = pageName;
        //    _activity = activity;
        //}
        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
        //    if (context.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        _UserService = (IServiceWrapper)context.HttpContext.RequestServices.GetService(typeof(IServiceWrapper));
        //        int currentUser = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //        if (!_UserService.Role.CheckUserAccess(currentUser, _pageName, _activity))
        //        {
        //            context.Result = new JsonResult((new BaseDTO(403, "دسترسی غیر مجاز", null)));
        //        }
        //    }
        //    else
        //    {
        //        context.Result = new JsonResult((new BaseDTO(403, "دسترسی غیر مجاز", null)));
        //    }
        //}
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                _UserService = (IServiceWrapper)context.HttpContext.RequestServices.GetService(typeof(IServiceWrapper));
                int currentUserRole = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.Role).Value);
                if (currentUserRole != 0)
                {
                    context.Result = new JsonResult((new BaseDTO(403, "Access denied", null)));
                }
            }
            else
            {
                context.Result = new JsonResult((new BaseDTO(403, "Access denied", null)));
            }
        }
    }
}
