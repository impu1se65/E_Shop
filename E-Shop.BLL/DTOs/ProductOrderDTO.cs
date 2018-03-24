using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.BLL.DTOs
{
    public class ProductOrderDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Size { get; set; }

        public string PhotoUrl { get; set; }

        public int Quantity { get;set; }

        public decimal SubPrice { get;set; }

    }
}
