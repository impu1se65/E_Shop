using E_Shop.BLL.Interfaces;
using E_Shop.Web.Areas.AdminPanel.Models.Error;
using E_Shop.Web.Areas.AdminPanel.Models.Order;
using E_Shop.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Web.Areas.AdminPanel.Controllers
{
    [CustomAuth("administrator")]
    public class OrderController : Controller
    {
        IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            var orders = _orderService.GetAllOrders();

            return View(orders);
        }

        [ChildActionOnly]
        public ActionResult ChangeOrderStatus(ChangeOrderStatusModel model)
        {
            return PartialView("ChangeOrderStatus", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ChangeOrderStatusModel model)
        {
            if (model != null)
            {
                var result = _orderService.ChangeOrderStatus(model.Id,model.OrderStatus);
                if (result.Succedeed)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Error", new ErrorViewModel { ErrorMessage = result.Message });
                }
            }

            return RedirectToAction("Index", "Error", new ErrorViewModel { ErrorMessage = "model is null"});
        }
            
    }
}