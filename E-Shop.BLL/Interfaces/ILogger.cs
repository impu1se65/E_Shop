using E_Shop.BLL.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.BLL.Interfaces
{
    public interface ILogger
    {
        void Log(Visitor visitor);
    }
}
