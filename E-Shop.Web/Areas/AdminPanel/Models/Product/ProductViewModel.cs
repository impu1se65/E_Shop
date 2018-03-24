using E_Shop.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Web.Areas.AdminPanel.Models.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        
        public decimal Price { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public string ProductDetails { get; set; }

        public string PhotoUrl { get; set; }

        public GenderDTO Gender { get; set; }

        public string Size { get; set; }

        public string Category { get; set; }

        public SizeCategory SizeCategory { get;set; }
    }
}