using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shop.DAL.EF;
using E_Shop.DAL.Identity;
using E_Shop.DAL.Interfaces;
using E_Shop.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace E_Shop.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        EShopDbContext _db;
        GenericRepository<Order> _orderRepository;
        GenericRepository<Product> _productRepository;
        EShopRoleManager _roleManager;
        EShopUserManager _userManager;

        /// <summary>
        /// Initialize new instance EFUnitOfWork
        /// </summary>
        /// <param name="connectionString">Connection string for entity framework</param>
        public EFUnitOfWork(string connectionString)
        {
            _db = new EShopDbContext(connectionString);
            _roleManager = new EShopRoleManager(new RoleStore<Role>(_db));
            _userManager = new EShopUserManager(new UserStore<Customer>(_db));
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
           await  _db.SaveChangesAsync();
        }

        public UserManager<Customer> UserManager
        {
            get=>_userManager;
        }
       
        
        public RoleManager<Role> RoleManager
        {
            get => _roleManager; 
        }

        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new GenericRepository<Product>(_db);
                return _productRepository;
            }
            
        }

        public IGenericRepository<Order> OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new GenericRepository<Order>(_db);
                return _orderRepository;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
