using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace E_Shop.Web.Util
{
    public class LogExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled )
            {
                var exceptionModel = new ExceptionModel
                {
                    ErrorMessage=exceptionContext.Exception.Message,
                    RouteData=exceptionContext.RouteData.ToString(),
                    Exception= exceptionContext.Exception.ToString(),
                    Date=DateTimeOffset.UtcNow,
                };
                string json = JsonConvert.SerializeObject(exceptionModel,Formatting.Indented);

                System.IO.File.AppendAllText(HostingEnvironment.MapPath("~/Logs/exceptions.txt"), json);
                exceptionContext.Result = new RedirectToRouteResult(
                     new System.Web.Routing.RouteValueDictionary {
                          {"area","" } ,{ "controller", "Error" }, { "action", "Index" },{ "ErrorMessage", "Something went wrong."}
                     });

                exceptionContext.ExceptionHandled = true;
            }
        }
    }


    public class ExceptionModel
    {
        public string Exception { get; set; }
        public string ErrorMessage { get; set; }
        public string RouteData { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}