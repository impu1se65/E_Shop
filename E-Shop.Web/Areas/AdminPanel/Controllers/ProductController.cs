using E_Shop.BLL.DTOs;
using E_Shop.BLL.Interfaces;
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
    [CustomAuth("administrator", "moderator")]
    public class ProductController : Controller
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            var products = _productService.GetAllProductsWithRepeats();
            var productsview = new List<IndexProductModel>();
            foreach (var product in products)
            {
                productsview.Add(new IndexProductModel
                {
                    Id=product.Id,
                    Name=product.Name,
                    PhotoUrl=product.PhotoUrl,
                    Price=product.Price,
                    Size=product.Size,
                    Color=product.Color,
                });
            }
            return View(productsview);
        }

        public ActionResult Details(int id)
        {
            return View();
        }
        private SizeCategory ReturnSizeCategory()
        {
            var sizes = new List<SelectListItem>();
            var category=new List<SelectListItem>();
            SizeCategory sizeCategory = new SizeCategory();
            sizes.Add(new SelectListItem
            {
                Value="XS",
                Text ="XS"
            });
            sizes.Add(new SelectListItem
            {
                Value = "S",
                Text = "S"
            });
            sizes.Add(new SelectListItem
            {
                Value = "M",
                Text = "M"
            });
            sizes.Add(new SelectListItem
            {
                Value = "L",
                Text = "L"
            });
            sizes.Add(new SelectListItem
            {
                Value = "XL",
                Text = "XL"
            });
            sizes.Add(new SelectListItem
            {
                Value = "2XL",
                Text = "2XL"
            });
            sizes.Add(new SelectListItem
            {
                Value = "3XL",
                Text = "3XL"
            });

            category.Add(new SelectListItem
            {
                Text="Shirts",
                Value="Shirts"
            });
            category.Add(new SelectListItem
            {
                Text = "Jumpers and Cardigans",
                Value = "Jumpers and Cardigans"
            });
            category.Add(new SelectListItem
            {
                Text = "Hoodies and Sweatshirts",
                Value = "Hoodies and Sweatshirts"
            });
            category.Add(new SelectListItem
            {
                Text = "Jackets and Coats",
                Value = "Jackets and Coats"
            });
            sizeCategory.Category = category;
            sizeCategory.Sizes = sizes;

            return sizeCategory;
        }

        public ActionResult Create()
        {
            ProductViewModel model = new ProductViewModel();
            model.SizeCategory = ReturnSizeCategory();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductPostViewModel model)
        {
            if(ModelState.IsValid)
            {
                var productDTO = new ProductDTO()
                {
                    Category = model.Category,
                    Color = model.Color,
                    Date = DateTimeOffset.Now,
                    Gender = model.Gender,
                    Name = model.Name,
                    PhotoUrl = model.PhotoUrl,
                    Price = model.Price,
                    ProductDetails = model.ProductDetails,
                    Size = model.Size,
                };

                var result= _productService.AddNewProduct(productDTO);

                if (result.Succedeed)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Error", new ErrorViewModel { ErrorMessage = result.Message });
                }

            }

            ProductViewModel modelview = new ProductViewModel()
            {
               Color=model.Color,
               Name=model.Name,
               PhotoUrl=model.PhotoUrl,
               Price=model.Price,
               ProductDetails=model.ProductDetails,
               Gender=model.Gender,
               Category=model.Category,
               Size=model.Size,
            };
            modelview.SizeCategory = ReturnSizeCategory();
            return View(modelview);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var product = _productService.GetProductById((int)id);

            if(product==null)
            {
                return HttpNotFound();
            }

            var viewmodel = new ProductViewModel()
            {
                Id=product.Id,
                Color = product.Color,
                Gender = product.Gender,
                Name = product.Name,
                PhotoUrl = product.PhotoUrl,
                Price = product.Price,
                ProductDetails = product.ProductDetails,
            };

            viewmodel.SizeCategory = ReturnSizeCategory();

            foreach (var item in viewmodel.SizeCategory.Category)
            {
                if (item.Text == product.Category)
                    item.Selected = true;
            }

            foreach (var item in viewmodel.SizeCategory.Sizes)
            {
                foreach (var size in product.Sizes)
                {
                    if (size.ProductId == product.Id)
                    {
                        if (item.Text == size.Size)
                            item.Selected = true;
                    }
                }
            }      
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(ProductPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new ProductDTO()
                {
                    Id = model.Id,
                    Color = model.Color,
                    Gender = model.Gender,
                    Name = model.Name,
                    PhotoUrl = model.PhotoUrl,
                    Price = model.Price,
                    ProductDetails = model.ProductDetails,
                    Size = model.Size,
                    Category = model.Category,
                };

                var result = _productService.Edit(product);

                if (result.Succedeed)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index","Error", new ErrorViewModel { ErrorMessage = result.Message });
                }
            }

            ProductViewModel modelview = new ProductViewModel()
            {
                Color = model.Color,
                Name = model.Name,
                PhotoUrl = model.PhotoUrl,
                Price = model.Price,
                ProductDetails = model.ProductDetails,
                Gender = model.Gender,
                Category = model.Category,
                Size = model.Size,
            };
            modelview.SizeCategory = ReturnSizeCategory();

            return View(modelview);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound(); 
            }

            var result = _productService.DeleteProduct((int)id);

            if(result.Succedeed)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Error", new ErrorViewModel { ErrorMessage = result.Message });
        }
     
    }
}
