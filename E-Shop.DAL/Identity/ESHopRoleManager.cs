using E_Shop.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.DAL.Identity
{
    public class EShopRoleManager : RoleManager<Role>
    {
        public EShopRoleManager(RoleStore<Role> store) : base(store)
        {

        }
    }
}
