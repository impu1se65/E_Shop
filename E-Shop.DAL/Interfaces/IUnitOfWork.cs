using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shop.DAL.Identity;
using E_Shop.DAL.Models;
using Microsoft.AspNet.Identity;

namespace E_Shop.DAL.Interfaces
{
    /// <summary>
    /// Provides access to repositories with 1 db context
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Provides access to role manager
        /// </summary>
        RoleManager<Role> RoleManager { get; }

        /// <summary>
        /// Provides access to user manager
        /// </summary>
        UserManager<Customer> UserManager { get; }

        /// <summary>
        /// Provides access to product repository
        /// </summary>
        IGenericRepository<Product> ProductRepository { get; }

        /// <summary>
        /// Provides access to order
        /// </summary>
        IGenericRepository<Order> OrderRepository { get; }

        /// <summary>
        /// Synchronized save changes in db
        /// </summary>
        void Save();

        /// <summary>
        /// Asynchronized save changes in db
        /// </summary>
        Task SaveAsync();

        void Dispose();
    }
}
