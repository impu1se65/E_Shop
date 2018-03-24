using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Web.Util
{
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;

        public CustomAuthAttribute(params string[] roles)
        {
            allowedroles = roles;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isRole = false;
            bool auth = filterContext.HttpContext.User.Identity.IsAuthenticated;
            if (auth)
            {
                foreach (var role in allowedroles)
                {
                    if (filterContext.HttpContext.User.IsInRole(role))
                    {
                        isRole = true;
                    }
                }

                if (isRole == false)
                {
                    filterContext.Result = new RedirectToRouteResult(
                      new System.Web.Routing.RouteValueDictionary {
                          {"area","" } ,{ "controller", "Error" }, { "action", "Index" },{ "ErrorMessage","You don`t have access"}

                  });
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                     new System.Web.Routing.RouteValueDictionary {
                          {"area","" } ,{ "controller", "Error" }, { "action", "Index" },{ "ErrorMessage","You don`t have access"}

                 });
            }
        }
    }
}