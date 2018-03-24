using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Web.Areas.AdminPanel.Models.Product
{
    public class SizeCategory
    {
        public IEnumerable<SelectListItem> Sizes { get; set; }

        public IEnumerable<SelectListItem> Category { get; set; }
    }
}