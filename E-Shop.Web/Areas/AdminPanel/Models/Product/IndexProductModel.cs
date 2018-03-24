using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Shop.Web.Areas.AdminPanel.Models.Product
{
    public class IndexProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public string PhotoUrl { get; set; }

    }
}