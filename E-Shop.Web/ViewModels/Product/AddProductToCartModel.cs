using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Web.ViewModels.Product
{
    public class AddProductToCartModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(minimum:1,maximum:100)]
        public int Quantity { get; set; }

        public bool? ProductAdded { get; set; }

        public IEnumerable<SelectListItem> Sizes { get; set; }
    }
}