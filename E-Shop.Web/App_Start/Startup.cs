using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using E_Shop.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(E_Shop.Web.App_Start.Startup))]

namespace E_Shop.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.CreatePerOwinContext(CreateCustomerService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private ICustomerService CreateCustomerService()
        {
            return DependencyResolver.Current.GetService<ICustomerService>();
        }
    }
}
