using Autofac;
using E_Shop.DAL.Interfaces;
using E_Shop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.BLL.Infrastucture
{
    public class AutofacModule:Module
    {
        private string _connectionString;

        public AutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new EFUnitOfWork(_connectionString)).As<IUnitOfWork>().InstancePerRequest();
            base.Load(builder);
        }
    }
}
