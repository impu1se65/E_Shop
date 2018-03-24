using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTimeOffset OrderDate { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

    public enum OrderStatus
    {
        Registered,
        Paid ,
        Canceled ,
    }
}
