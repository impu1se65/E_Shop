using E_Shop.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Shop.Web.Areas.AdminPanel.Models.Order
{
    public class ChangeOrderStatusModel
    {
        public int Id { get; set; }

        public OrderStatusDTO OrderStatus { get; set; }
    }
}