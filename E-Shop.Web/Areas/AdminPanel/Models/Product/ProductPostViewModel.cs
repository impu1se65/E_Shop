using E_Shop.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Shop.Web.Areas.AdminPanel.Models.Product
{
    public class ProductPostViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(minimum: 1,maximum:Int32.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Color { get; set; }

        [Required]
        [StringLength(350, MinimumLength = 2)]
        public string ProductDetails { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [Required]
        public GenderDTO Gender { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public string Category { get; set; }
    }
}