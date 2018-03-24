using E_Shop.BLL.DTOs;
using E_Shop.BLL.Interfaces;
using E_Shop.Web.ViewModels;
using E_Shop.Web.ViewModels.Product;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PagedList;

namespace E_Shop.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        ICustomerService _customerService;

        public ProductController(IProductService productService,ICustomerService customerService)
        {
            _productService = productService;
            _customerService = customerService;

        }

        [HttpGet]
        public ActionResult Index(int page=1)
        {
            if (_customerService.IsUserAuthorizeOrExist(HttpContext.User.Identity.GetUserId())==false)
            {
                HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }

            var list = _productService.GetAllProducts();

            var productList = new List<IndexProductModel>();

                foreach (var item in list)
                {
                    productList.Add(new IndexProductModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        PhotoUrl = item.PhotoUrl,
                    });
                }

          /*  int pageSize = 8;
            var pagedList=productList.Skip((page - 1) * pageSize).Take(pageSize);
            IndexViewModel model = new IndexViewModel {Products=pagedList };
            model.PageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = productList.Count,
            };
            */
               IndexViewModel model = new IndexViewModel {Products=productList};
            return View(model);            
        }


        [ChildActionOnly]
        public ActionResult SortMenu(IndexPartialView viewModel)//4 methods getProductMenu
        {
            List<SelectListItem> sortMenu = new List<SelectListItem>();
            List<SelectListItem> categoriesMenu = new List<SelectListItem>() { new SelectListItem { Text = "Categories", Value = "" } };
            List<SelectListItem> colorsMenu = new List<SelectListItem>() { new SelectListItem { Text = "Colors", Value = "" } };
            List<SelectListItem> sizesMenu = new List<SelectListItem>() { new SelectListItem { Text = "Sizes", Value = "" } };

            sortMenu.Add(new SelectListItem { Text = "Sort", Value = "Date" });
            sortMenu.Add(new SelectListItem { Text = "What`s new", Value = "Date" });
            sortMenu.Add(new SelectListItem { Text = "What`s old", Value = "Date ByDescending" });
            sortMenu.Add(new SelectListItem { Text = "Price high to low", Value = "Price ByDescending" });
            sortMenu.Add(new SelectListItem { Text = "Price low to high", Value = "Price" });
            sortMenu.Add(new SelectListItem { Text = "Name(Az-za)", Value = "Name" });
            sortMenu.Add(new SelectListItem { Text = "Name(Za-az)", Value = "Name ByDescending" });

            foreach(var item in sortMenu)
            {
                if (item.Value == viewModel.OrderParam)
                    item.Selected = true;
            }
                
            var menu = _productService.GetAllMenus();

            foreach (var item in menu.Categories)
            {
                categoriesMenu.Add(new SelectListItem
                {
                    Text=item,
                    Value=item,
                });
            }

            foreach(var item in categoriesMenu)
            {
                if (item.Value == viewModel.CategoryParam)
                    item.Selected = true;
            }

            foreach (var item in menu.Colors)
            {
                colorsMenu.Add(new SelectListItem
                {
                    Text = item,
                    Value = item,
                });
            }

            foreach (var item in colorsMenu)
            {
                if (item.Value == viewModel.ColorParam)
                    item.Selected = true;
            }

            foreach (var item in menu.Sizes)
            {
                sizesMenu.Add(new SelectListItem
                {
                    Text = item,
                    Value = item,
                });
            }

            foreach (var item in sizesMenu)
            {
                if (item.Value == viewModel.SizeParam)
                    item.Selected = true;
            }

          MultiSelectList multiple = new MultiSelectList(menu.Categories);

            IndexPartialView view = new IndexPartialView
            {
                SortList = sortMenu,
                CategoriesList=categoriesMenu,
                ColorsList=colorsMenu,
                SizesList=sizesMenu,
                MultipleCategoriesList=multiple,
            };

            return PartialView("SortMenuIndex", view);
        }

        [HttpPost]
        public ActionResult Index(IndexPartialView model)
        {
            var multiple = model.MultipleParam;
            var  list = _productService.GetProductsBySize(model.SizeParam);          
            list = _productService.GetProductsByCategory(model.CategoryParam, list);       
            list = _productService.GetProductsByColor(model.ColorParam, list);
            list = _productService.GetProductsByPrice(model.MinPrice, model.MaxPrice, list);
            list = _productService.GetProductsByName(model.SearchQuery,list);

            if (!String.IsNullOrEmpty(model.OrderParam))
            {
                if (model.OrderParam.Contains("ByDescending"))
                {
                    string order = model.OrderParam.Split(' ')[0];
                    list = _productService.SortByParam(list, order, true);
                }
                else
                {
                    list = _productService.SortByParam(list, model.OrderParam, false);
                }
            }

            var productList = new List<IndexProductModel>();

            foreach (var item in list)
            {
                productList.Add(new IndexProductModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    PhotoUrl = item.PhotoUrl,
                });
            }

            var viewModel = new IndexViewModel()
            {
                Products=productList.ToPagedList(1,9),
                CategoryParam= model.CategoryParam,
                OrderParam=model.OrderParam,
                ColorParam=model.ColorParam,
                SizeParam=model.SizeParam,
                MaxPrice=model.MaxPrice,
                MinPrice=model.MinPrice,
                SearchQuery=model.SearchQuery,
            };

            return View(viewModel);
        }

        public ActionResult Details(int id,bool? productAdded)
        {
            var product = _productService.GetProductById(id);
            var productViewModel = new DetailsProductModel
            {
                Category = product.Category,
                Color = product.Color,
                Name = product.Name,
                PhotoUrl = product.PhotoUrl,
                Price = product.Price,
                ProductDetails=product.ProductDetails,
            };

            if (product == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> sizes = new List<SelectListItem>();
            foreach (var item in product.Sizes)
            {
                sizes.Add(new SelectListItem
                {
                    Text = item.Size,
                    Value = item.ProductId.ToString(),
                });
            }
            sizes.First().Selected = true ;
            productViewModel.Sizes = sizes;
            productViewModel.ProductAdded = productAdded;
            return View(productViewModel);
        }

        [ChildActionOnly]
        public ActionResult DetailsPartialView(AddProductToCartModel model)
        {
            return PartialView("DetailsPartialView",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProductToCart(AddProductToCartModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _customerService.AddProductToCart(new SerializeProductInfoForCart
                {
                    Id = model.ProductId,
                    Quantity = model.Quantity
                }, HttpContext);

                if (!result.Succedeed)
                {
                    return RedirectToAction("Index", "Error", new ErrorViewModel { ErrorMessage = result.Message });
                }

                return RedirectToAction("Details", new { id = model.ProductId, productAdded = true });
            }
             return RedirectToAction("Details",new { id=model.ProductId});
        }

       
    }
}
 