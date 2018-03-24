using E_Shop.BLL.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace E_Shop.BLL.Loggers
{
    public class JsonLogger : ILogger
    {
        public void Log(Visitor visitor)
        {
            string json = JsonConvert.SerializeObject(visitor, Formatting.Indented);
            System.IO.File.AppendAllText(HostingEnvironment.MapPath("~/Logs/visitorsJSON.txt"), json);
        }
    }
}
