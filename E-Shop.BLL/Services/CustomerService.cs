using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using E_Shop.BLL.DTOs;
using E_Shop.BLL.Infrastucture;
using E_Shop.BLL.Interfaces;
using E_Shop.DAL.Interfaces;
using E_Shop.DAL.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace E_Shop.BLL.Services
{
    public class CustomerService:ICustomerService
    {
        IUnitOfWork _db;

        /// <summary>
        /// Initialize new Customer Service instance
        /// </summary>
        /// <param name="db">IUnitOfWork instance</param>
        public CustomerService(IUnitOfWork db)
        {
            _db = db;
        }

        public bool IsUserAuthorizeOrExist(string userId)
        {
            var user = _db.UserManager.FindById(userId);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsUserBanned(string userId)
        {
            var user = _db.UserManager.FindById(userId);
            return user.IsBanned.HasValue&& user.IsBanned.Value;
            //if (user.IsBanned==true)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public IEnumerable<CustomerDTO> GetAllUsers()
        {
           var users= _db.UserManager.Users.ToList();
            var userDTOs = new List<CustomerDTO>();
          
            foreach (var item in users)
            {
                CustomerDTO customer = new CustomerDTO
                {
                    Address = item.Address,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Id = item.Id,
                    IsBanned = item.IsBanned,
                    UserName = item.UserName,
                    OrdersCount=item.Orders.Count,
                };
                
                userDTOs.Add(customer);
            }

            return userDTOs;
        }

        public OperationDetails AddProductToCart(SerializeProductInfoForCart info, HttpContextBase httpContext)
        {
            if (info.Id != 0 && info.Quantity != 0)
            {
                if (httpContext.Request.Cookies["Cart"] == null)
                {
                    /*
                     * Create serialize product info for cart object
                     * Add information in collection
                     * Serialize collection in json
                     * Add json string in cookie for use like shopping cart
                     */

                    var list = new List<SerializeProductInfoForCart>();
                    list.Add(info);
                    string json = JsonConvert.SerializeObject(list, Formatting.None);
                    HttpCookie listCookie = new HttpCookie("Cart", json);
                    listCookie.Expires = DateTime.Now.AddDays(1);
                    httpContext.Response.Cookies.Add(listCookie);

                    return new OperationDetails(true, "Operation was successfully completed", "");
                }
                else
                {
                    /*
                     * Get "Cart" cookie value
                     * Deserialize in List<SerializeProductInfoForCart>
                     * Check if product what add in cart already exist in shopping cart
                     * If exist update quantity value
                     * Else add value to list
                     * Serialize collection in json
                     * Add json string in cookie for use like shopping cart
                     */

                    var json = httpContext.Request.Cookies["Cart"].Value;
                    var list = JsonConvert.DeserializeObject<List<SerializeProductInfoForCart>>(json);
                    bool isExistInCart = false;
                    foreach (var item in list)
                    {
                        if (item.Id == info.Id)
                        {
                            item.Quantity+=info.Quantity;
                            isExistInCart = true;
                        }
                        
                    }
                    if (isExistInCart)
                    {
                        json = JsonConvert.SerializeObject(list, Formatting.None);
                        HttpCookie listCookie = new HttpCookie("Cart", json);
                        listCookie.Expires = DateTime.Now.AddDays(1);
                        httpContext.Response.Cookies.Add(listCookie);
                        return new OperationDetails(true, "Operation was successfully completed", "");
                    }
                    else
                    {
                        list.Add(info);
                        json = JsonConvert.SerializeObject(list, Formatting.None);
                        HttpCookie listCookie = new HttpCookie("Cart", json);
                        listCookie.Expires = DateTime.Now.AddDays(1);
                        httpContext.Response.Cookies.Add(listCookie);
                        return new OperationDetails(true, "Operation was successfully completed", "");
                    }
                }
            }
            else
            {
                return new OperationDetails(false, "Operation was failed", "Cart info = 0");
            }
        }

        public OperationDetails RemoveProductFromCart(int id, HttpContextBase httpContext)
        {
            /*
             * Add "Cart" cookie value
             * Deserialize in List<SerializeProductInfoForCart>
             * Remove product from collection by id
             * Serialize list in json
             * Add in cookie
             */
            var json = httpContext.Request.Cookies["Cart"].Value;
            if (json == null)
            {
                return new OperationDetails(false, "Operation was failed", "Cart is empty");
            }
            var list = JsonConvert.DeserializeObject<List<SerializeProductInfoForCart>>(json);

            var itemToRemove = list.FirstOrDefault(item => item.Id == id);
            foreach (var item in list)
            {
                if (item.Id == id)
                {
                    list.Remove(item);
                    break;
                }
            }
            json = JsonConvert.SerializeObject(list, Formatting.None);
            HttpCookie listCookie = new HttpCookie("Cart", json);
            listCookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(listCookie);
            return new OperationDetails(true, "Operation was successfully completed", "");

        }

        public CartDTO GetUserCart(HttpContextBase httpContext)
        {
            if (httpContext.Request.Cookies["Cart"] == null)
            {
                return new CartDTO{ProductOrders= new List<ProductOrderDTO>() ,totalPrice=0};
            }
            else
            {
                CartDTO cart = new CartDTO();
                decimal totalPrice = 0;
                var json = httpContext.Request.Cookies["Cart"].Value;
                var cookieList = JsonConvert.DeserializeObject<List<SerializeProductInfoForCart>>(json);
                var list = new List<ProductOrderDTO>();
                foreach (var item in cookieList)
                {
                    var product=_db.ProductRepository.FindById(item.Id);
                    var productOrderDTO=new ProductOrderDTO
                    {
                        Id = product.Id,
                        Quantity = item.Quantity,

                        Name = product.Name,
                        PhotoUrl = product.PhotoUrl,
                        Price = product.Price,       
                        Size=product.Size,
                        SubPrice=product.Price*item.Quantity,
                    };                  
                    list.Add(productOrderDTO);

                    totalPrice += item.Quantity * product.Price;
                }
                cart.totalPrice = totalPrice;
                cart.ProductOrders = list;
                return cart;
            }
        }    

        public async Task<ClaimsIdentity> Authenticate(CustomerDTO userDto)
        {
            ClaimsIdentity claim = null;
            Customer customer = await _db.UserManager.FindAsync(userDto.Email,userDto.Password);
            if (customer != null)
                claim = await _db.UserManager.CreateIdentityAsync(customer,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;

        }

        public async Task<OperationDetails> Create(CustomerDTO userDto)
        {
            Customer customer=await _db.UserManager.FindByEmailAsync(userDto.Email);
            if (customer == null)
            {
                customer = new Customer
                {
                    Email =userDto.Email,
                    UserName =userDto.Email,
                    Address =userDto.Address,
                    IsBanned=false,
                    FirstName=userDto.FirstName,
                    LastName=userDto.LastName,                                
                };

                var result = await _db.UserManager.CreateAsync(customer,userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await _db.UserManager.AddToRoleAsync(customer.Id,userDto.Role);

                await _db.SaveAsync();

                return new OperationDetails(true, "Operation was successfully completed", "");

            }
            else
            {
                return new OperationDetails(false,"User already exists","Email");
            }
        }

        public OperationDetails BanCustomer(string userId)
        {
            var customer = _db.UserManager.FindByIdAsync(userId).Result;

            if (customer == null)
            {
                return new OperationDetails(false, "Operation was failed", "customer is null");
            }

            customer.IsBanned = true;
            _db.Save();
            return new OperationDetails(true, "Operation was successfully completed", "");
        }

        public OperationDetails UnbanCustomer(string userId)
        {
            var customer = _db.UserManager.FindByIdAsync(userId).Result;

            if (customer == null)
            {
                return new OperationDetails(false, "Operation was failed", "customer is null");
            }

            customer.IsBanned = false;
            _db.Save();
            return new OperationDetails(true, "Operation was successfully completed", "");
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
