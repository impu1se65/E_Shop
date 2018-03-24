using E_Shop.BLL.Loggers;
using E_Shop.Web.Util;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogAttribute(new JsonLogger()));
          //  filters.Add(new LogExceptionAttribute());
        }
    }
}
