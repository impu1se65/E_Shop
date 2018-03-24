using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Web.ViewModels.Product
{
    public class DetailsProductModel
    {
        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public string ProductDetails { get; set; }

        public string PhotoUrl { get; set; }

        public bool? ProductAdded { get; set; }

        public IEnumerable<SelectListItem> Sizes { get; set; }
    }
}