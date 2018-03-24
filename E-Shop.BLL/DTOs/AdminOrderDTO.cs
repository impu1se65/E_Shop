using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.BLL.DTOs
{
    public class AdminOrderDTO
    {
        public int Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy/HH:mm}")]
        public DateTimeOffset OrderDate { get; set; }

        public OrderStatusDTO OrderStatus { get; set; }

        public IEnumerable<ProductOrderDTO> OrderItems { get; set; }

        public decimal TotalCost { get; set; }

        public string Email { get; set; }

     
    }
}
