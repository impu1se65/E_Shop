using E_Shop.DAL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.DAL.Identity
{
    public class EShopUserManager : UserManager<Customer>
    {
        public EShopUserManager(IUserStore<Customer> store) : base(store)
        {

        }
    }
}
