using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace E_Shop.BLL.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public string ProductDetails { get; set; }

        public string PhotoUrl { get; set; }

        public GenderDTO Gender { get; set; }

        public string Size { get; set; }

    }

    public enum GenderDTO
    {
        Men,
        Women,
        Unisex,
    }
}
