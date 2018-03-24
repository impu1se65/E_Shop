using E_Shop.Web.Areas.AdminPanel.Models.Error;
using E_Shop.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Web.Areas.AdminPanel.Controllers
{
    [CustomAuth("administrator", "moderator")]
    public class ErrorController : Controller
    {
        public ActionResult Index(ErrorViewModel model)
        {
            return View(model);
        }
    }
}