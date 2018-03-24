using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.BLL.DTOs
{
    public class CartDTO
    {
        public IEnumerable<ProductOrderDTO> ProductOrders { get; set; }

        public decimal totalPrice { get; set; }
    }
}
