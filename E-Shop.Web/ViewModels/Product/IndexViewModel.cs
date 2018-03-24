using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Shop.Web.ViewModels.Product
{
    public class IndexViewModel
    {
        public IEnumerable<IndexProductModel> Products { get; set; }

        public string OrderParam { get; set; }

        public string CategoryParam { get; set; }

        public string ColorParam { get; set; }

        public string SizeParam { get; set; }

        public string SearchQuery { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public PageInfo PageInfo { get; set; }

    }

    public class PageInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages  
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
}