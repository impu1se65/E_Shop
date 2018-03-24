using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Web.Areas.AdminPanel.Models.Customer
{
    public class BanViewModel
    {
        public string Id { get; set; }

        public bool? IsBanned { get; set; }

        public IEnumerable<SelectListItem> BanState { get; set; }
    }

   
}