using E_Shop.Web.Areas.AdminPanel.Models.Product;
using E_Shop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Web.Controllers
{
    public class ErrorController : Controller
    {
        
        public ActionResult Index(ErrorViewModel model)
        {
            return View(model);
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}