using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Shop.BLL.Interfaces;
using E_Shop.BLL.Services;
using Autofac;
using Autofac.Integration.Mvc;
using E_Shop.BLL.Infrastucture;
using System.Web.Mvc;
using System.Configuration;

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

    public class WebAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerRequest();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerRequest();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerRequest();
            base.Load(builder);
        }
    }

    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

          //  builder.RegisterFilterProvider();

          //  builder.RegisterSource(new ViewRegistrationSource());

            var stri= ConfigurationManager.AppSettings["connectionString"];
            builder.RegisterModule(new AutofacModule(ConfigurationManager.AppSettings["connectionString"]));
            builder.RegisterModule(new WebAutofacModule());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}