using E_Shop.BLL.Interfaces;
using E_Shop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Web.Controllers
{
    public class CartController : Controller
    {
        ICustomerService _customerService;

        public CartController(ICustomerService service)
        {
            _customerService = service;
        } 

        public ActionResult Index(string isSuccess)
        {
           var cart = _customerService.GetUserCart(HttpContext);
            ViewBag.isSuccess = isSuccess;
            return View(cart);
        }

        public ActionResult RemoveProductFromCart(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                var result = _customerService.RemoveProductFromCart((int)id,HttpContext);
                if (result.Succedeed)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Error", new ErrorViewModel { ErrorMessage = result.Message });
                }

            }
            
        }
    
    }
}