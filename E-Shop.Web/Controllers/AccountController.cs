using E_Shop.BLL.Interfaces;
using Microsoft.Owin.Security;
using System;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using E_Shop.Web.ViewModels.Account;
using E_Shop.BLL.DTOs;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Net;
using E_Shop.Web.ViewModels;

namespace E_Shop.Web.Controllers
{
    public class AccountController : Controller
    {
        private ICustomerService CustomerService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ICustomerService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        IOrderService _orderService;
        ICustomerService _customerService;

        public AccountController(IOrderService orderService,ICustomerService customerService)
        {
            _orderService = orderService;
            _customerService = customerService;
        }

        public ActionResult Index()
        {
            var userId = HttpContext.User.Identity.GetUserId();

            if (userId == null)
            {
                return RedirectToAction("Index", "Error", new ErrorViewModel { ErrorMessage = "User is not found. Please log in." });
            }

            var isBanned = _customerService.IsUserBanned(userId);

            if (isBanned)
            {
                return RedirectToAction("Index", "Error", new ErrorViewModel { ErrorMessage = "User is blocked" });
            }

            var orders = _orderService.GetAllUserOrders(userId);
            return View(orders);
        }

        public ActionResult Paid(int? id)
        {
            if(id==null)
            {
                return HttpNotFound();
            }

            PaidModel model = new PaidModel { Id = (int)id };
            return View(model);
        }

        [HttpPost]
        public ActionResult PaidForOrder(PaidModel model)
        {   
            _orderService.ChangeOrderStatus(model.Id, OrderStatusDTO.Paid);
            return RedirectToAction("Index");
        }

        public ActionResult MakeOrder()
        {
            var result = _orderService.MakeOrder(HttpContext);

            if (result.Succedeed)
            {
                return RedirectToAction("Index", "Cart", new { isSuccess = result.Message + ". " + result.Property });
            }
            else
            {
                return RedirectToAction("Index", "Cart", new { isSuccess = result.Message + ". " + result.Property });
            }
        }

        public ActionResult Login(string success)
        {
            ViewBag.Success = success;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                CustomerDTO dto = new CustomerDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await CustomerService.Authenticate(dto);

                if (claim == null)
                {
                    ModelState.AddModelError("", "Invalid login or password");
                }
                else
                {
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Product");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                CustomerDTO dto = new CustomerDTO
                {
                    Email = model.Email,
                    Password=model.Password,
                    Address = model.Address,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsBanned = false,
                    UserName=model.Email,
                    Role= "user",
                };
                var operationDetails = await CustomerService.Create(dto);

                if (operationDetails.Succedeed)
                    return RedirectToAction("Login",new { success="Thanks for registration"});
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
    }
}