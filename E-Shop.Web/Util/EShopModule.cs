using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Shop.BLL.Interfaces;
using E_Shop.BLL.Services;

namespace E_Shop.Web.Util
{
    public class EShopModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<ProductService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<ICustomerService>().To<CustomerService>();
        }
    }
}