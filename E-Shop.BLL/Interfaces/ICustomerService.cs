using E_Shop.BLL.DTOs;
using E_Shop.BLL.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace E_Shop.BLL.Interfaces
{
    /// <summary>
    /// Separates presentation layer from data access layer and imposes business rules for users logic
    /// </summary>
    public interface ICustomerService:IDisposable
    {
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="userDto">User data transfer object</param>
        /// <returns>Task operation details with result of operation</returns>
        Task<OperationDetails> Create(CustomerDTO userDto);

        /// <summary>
        /// Authenticate user in system
        /// </summary>
        /// <param name="userDto">User data transfer object</param>
        /// <returns>ClaimsIdentity task for authorize user</returns>
        Task<ClaimsIdentity> Authenticate(CustomerDTO userDto);

        /// <summary>
        /// Check if user exist or authorize
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Bool value.True if exist or authorize.Else return false</returns>
        bool IsUserAuthorizeOrExist(string userId);

        /// <summary>
        /// Checi if user is banned
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Bool value.True if banned.Else return false</returns>
        bool IsUserBanned(string userId);
        /// <summary>
        /// Block user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Operation details with result of operation</returns>
        OperationDetails BanCustomer(string userId);

        /// <summary>
        ///Unblock user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Operation details with result of operation</returns>
        OperationDetails UnbanCustomer(string userId);

        /// <summary>
        /// Return all users for admin panel
        /// </summary>
        /// <returns>Customer data transfer objects collection</returns>
        IEnumerable<CustomerDTO> GetAllUsers();

        /// <summary>
        /// Add product to shopping cart
        /// </summary>
        /// <param name="info">Type that encapsulated product ID and quantity</param>
        /// <param name="httpContext">Http context</param>
        /// <returns>>Operation details with result of operation</returns>
        OperationDetails AddProductToCart(SerializeProductInfoForCart info, HttpContextBase httpContext);

        /// <summary>
        /// Remove product from shopping cart
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="httpContext">Http context</param>
        /// <returns>Operation details with result of operation></returns>
        OperationDetails RemoveProductFromCart(int id, HttpContextBase httpContext);

        /// <summary>
        /// Return shopping cart
        /// </summary>
        /// <param name="httpContext">Http context</param>
        /// <returns>Cart transfer object what encapsulated products collection and cart total price</returns>
        CartDTO GetUserCart(HttpContextBase httpContext);
    }
}
