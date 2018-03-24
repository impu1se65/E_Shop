using E_Shop.BLL.Interfaces;
using E_Shop.BLL.Loggers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace E_Shop.Web.Util
{
    public class LogAttribute : ActionFilterAttribute
    {
        ILogger _logger;

        public LogAttribute(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            Visitor visitor = new Visitor()
            {
                Login = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "null",
                Ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                Url = request.RawUrl,
                Date = DateTime.UtcNow
            };

            _logger.Log(visitor);
          

            base.OnActionExecuting(filterContext);
        }
    }


   
}