using E_Shop.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shop.DAL.Interfaces;
using E_Shop.BLL.DTOs;
using E_Shop.BLL.Infrastucture;
using E_Shop.DAL.Models;
using System.Web;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace E_Shop.BLL.Services
{
    public class OrderService:IOrderService
    {
        IUnitOfWork _db;

        /// <summary>
        /// Initialize new Order Service instance
        /// </summary>
        /// <param name="db">IUnitOfWork instance</param>
        public OrderService(IUnitOfWork db)
        {
            _db = db;
        }

        public OperationDetails ChangeOrderStatus(int orderId,OrderStatusDTO orderStatusDTO)
        {
           var order= _db.OrderRepository.FindById(orderId);

            if(order==null)
            {
                return new OperationDetails(false, "Operation was failed", "order is null");
            }

            order.OrderStatus = (OrderStatus)(int)orderStatusDTO;
            _db.OrderRepository.Update(order);
            _db.Save();
            return new OperationDetails(true, "Operation was successfully completed", "");
        }

        public IEnumerable<AdminOrderDTO> GetAllOrders()
        {
            var ordersDTO = new List<AdminOrderDTO>();
            var orders = _db.OrderRepository.Get().ToList();
            /*
             * Mapping Order to AdminOrderDTO
             */
            foreach (var order in orders)
            {
                var orderDTO= new AdminOrderDTO
                {
                    Id=order.Id,
                    OrderDate=order.OrderDate,
                    OrderStatus=(OrderStatusDTO)(int)order.OrderStatus,
                    TotalCost=order.TotalCost,
                    Email=order.Customer.Email,
                };
                var list = new List<ProductOrderDTO>();
                foreach (var item in order.OrderItems)
                {
                    list.Add(new ProductOrderDTO
                    {
                        Id=item.Product.Id,
                        Name=item.Product.Name,
                        PhotoUrl= item.Product.PhotoUrl,
                        Price= item.Product.Price,
                        Size=item.Product.Size,
                        SubPrice= item.Product.Price*item.Quantity,
                        Quantity=item.Quantity
                    });
                }
                orderDTO.OrderItems = list;
                ordersDTO.Add(orderDTO);
            }
            return ordersDTO;
        }

        public IEnumerable<OrderDTO> GetAllUserOrders(string userId)
        {
           var customer=_db.UserManager.FindByIdAsync(userId).Result;

            if(customer == null)
            {
                return null;
            }
           
            var orderDTOList = new List<OrderDTO>();
            foreach (var item in customer.Orders)
            {
                var orderDTO = new OrderDTO
                {
                    Id=item.Id,
                    OrderDate=item.OrderDate,
                    OrderStatus=(OrderStatusDTO)(int)item.OrderStatus,
                    
                };
                var orderItemDTO = new List<ProductOrderDTO>();
                foreach (var orderItem in item.OrderItems)
                {
                    orderItemDTO.Add(new ProductOrderDTO
                    {
                        Quantity=orderItem.Quantity,
                        Id=orderItem.Product.Id,
                        Name=orderItem.Product.Name,
                        PhotoUrl=orderItem.Product.PhotoUrl,
                        Price=orderItem.Product.Price,
                        Size=orderItem.Product.Size,
                        SubPrice=orderItem.Product.Price*orderItem.Quantity,
                    });                  
                }
                orderDTO.OrderItems = orderItemDTO;
                orderDTO.TotalCost = item.TotalCost;
                orderDTOList.Add(orderDTO);
            }
            return orderDTOList;

        }

        public OperationDetails MakeOrder(HttpContextBase httpContext)
        {
            var customer = _db.UserManager.FindByIdAsync(httpContext.User.Identity.GetUserId()).Result ;
            if (customer == null)
            {
                return new OperationDetails(false, "Operation was failed", "Customer is not authorize");
            }

            if (customer.IsBanned==true)
            {
                return new OperationDetails(false, "Operation was failed", "Customer is blocked");
            }

            Order order = new Order {
                                     OrderDate = DateTimeOffset.UtcNow,
                                     OrderStatus = OrderStatus.Registered ,
                                     TotalCost = 0
                                    };
            /*
             * Get shopping cart info from cookie
             * Deserialize from json to List<SerializeProductInfoForCart>
             * Add new order
             * Delete items from shopping cart
             */
            var json = httpContext.Request.Cookies["Cart"].Value;

            if (json == null)
            {
                return new OperationDetails(false, "Operation was failed", "Cart is empty");
            }
            var cookieList = JsonConvert.DeserializeObject<List<SerializeProductInfoForCart>>(json);
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in cookieList)
            {
                orderItems.Add(new OrderItem
                {
                    Quantity=item.Quantity,
                    Product=_db.ProductRepository.FindById(item.Id),
                });
            }
            foreach (var item in orderItems)
            {
                order.TotalCost += item.Quantity * item.Product.Price;
            }
            order.OrderItems = orderItems;
            order.Customer = customer;
            _db.OrderRepository.Create(order);
            _db.Save();
            httpContext.Response.Cookies["Cart"].Expires = DateTime.Now.AddDays(-1);
            return new OperationDetails(true, "Operation was successfully completed", "");
        }
    }
}
