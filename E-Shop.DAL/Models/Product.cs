using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.DAL.Models
{
    public class Product
    {      
        public int Id { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string ProductDetails { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string Size { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

   

    public enum Gender
    {
        Men,
        Women,
        Unisex,
    }
}
