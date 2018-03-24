using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.BLL.DTOs
{
    public class SelectMenusDTO
    {
       public IEnumerable<string> Categories { get; set; }

       public IEnumerable<string> Sizes { get; set; }

       public IEnumerable<string> Colors { get; set; }       
    }
}
