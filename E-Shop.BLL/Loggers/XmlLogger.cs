using E_Shop.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace E_Shop.BLL.Loggers
{
    public class XmlLogger : ILogger
    {
        public void Log(Visitor visitor)
        {
            XmlSerializer formater = new XmlSerializer(typeof(Visitor));

            using (FileStream filestream = new FileStream(HostingEnvironment.MapPath("~/Logs/visitorsXML.txt"), FileMode.OpenOrCreate))
            {
                formater.Serialize(filestream, visitor);
            }

        }
    }
}

