using E_Shop.BLL.Interfaces;
using E_Shop.Web.Areas.AdminPanel.Models.Customer;
using E_Shop.Web.Areas.AdminPanel.Models.Error;
using E_Shop.Web.Areas.AdminPanel.Models.Product;
using E_Shop.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Web.Areas.AdminPanel.Controllers
{
   [CustomAuth("administrator")]
    public class CustomerController : Controller
    {
        ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public ActionResult Index()
        {
            var customers = _customerService.GetAllUsers().ToList();
            var list = new List<CustomerViewModel>();

            foreach (var customer in customers)
            {
                CustomerViewModel model = new CustomerViewModel
                {
                    Id = customer.Id,
                    Address = customer.Address,
                    Name = customer.FirstName + " " + customer.LastName,
                    Email = customer.Email,
                    OrderCount = customer.OrdersCount,
                    IsBanned = customer.IsBanned,
                };
                var BanState = new List<SelectListItem>()
                {
                    new SelectListItem
                    {
                        Text="Banned",
                        Value="true",
                    },
                    new SelectListItem
                    {
                        Text="Unbanned",
                        Value="false",
                    }
                 };
                if (model.IsBanned == true)
                {
                    BanState[0].Selected = true;
                }
                else
                {
                    BanState[1].Selected = true;
                }
                model.BanState = BanState;
                list.Add(model);
            }

            return View(list);
        }

        [ChildActionOnly]
        public ActionResult BanPartialView(BanViewModel model)
        {
            return PartialView("BanPartialView", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(BanViewModel model)
        {
            if (model.IsBanned==true)
            {
                var result = _customerService.BanCustomer(model.Id);

                if (result.Succedeed)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Error", new ErrorViewModel { ErrorMessage = result.Message} );
                }
            }

            if (model.IsBanned==false)
            {
                var result = _customerService.UnbanCustomer(model.Id);
                if (result.Succedeed)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Error", new ErrorViewModel { ErrorMessage = result.Message });
                }
            }

            return RedirectToAction("Index");
        }
    }
}