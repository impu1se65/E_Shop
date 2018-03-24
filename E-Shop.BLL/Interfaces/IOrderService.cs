using E_Shop.BLL.DTOs;
using E_Shop.BLL.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace E_Shop.BLL.Interfaces
{
    /// <summary>
    /// Separates presentation layer from data access layer and imposes business rules for order items
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Make order (create order item and delete items from shopping cart)
        /// </summary>
        /// <param name="httpContext">Http context</param>
        /// <returns>Operation details with result of operation</returns>
        OperationDetails MakeOrder(HttpContextBase httpContext);

        /// <summary>
        /// Change order status
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <param name="orderStatusDTO">Order status</param>
        /// <returns>Operation details with result of operation</returns>
        OperationDetails ChangeOrderStatus(int orderId,OrderStatusDTO orderStatusDTO);

        /// <summary>
        /// Return all user orders
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Order data transfer object collection</returns>
        IEnumerable<OrderDTO> GetAllUserOrders(string userId);

        /// <summary>
        /// Return all orders for admin panel
        /// </summary>
        /// <returns>Orders collection</returns>        
        IEnumerable<AdminOrderDTO> GetAllOrders();
    }
}
