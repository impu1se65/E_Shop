using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Web.ViewModels.Product
{
    public class IndexPartialView
    {
        public IEnumerable<SelectListItem> SortList { get; set; }

        public IEnumerable<SelectListItem> CategoriesList { get; set; }

        public IEnumerable<SelectListItem> SizesList { get; set; }

        public IEnumerable<SelectListItem> ColorsList { get; set; }

        public MultiSelectList MultipleCategoriesList { get; set; }

        public IEnumerable<string> MultipleParam { get; set; }

        public string OrderParam { get; set; }

        public string CategoryParam { get; set; }

        public string ColorParam { get; set; }

        public string SizeParam { get; set; }

        public string SearchQuery { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }
    }
}