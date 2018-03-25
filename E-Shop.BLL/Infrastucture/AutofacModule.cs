using Autofac;
using AutoMapper;
using E_Shop.DAL.Interfaces;
using E_Shop.DAL.Repositories;
using NLog;
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
            var config = new MapperConfiguration(cfg=>cfg.AddProfile<DtoProfile>());
            builder.Register(c => new EFUnitOfWork(_connectionString)).As<IUnitOfWork>().InstancePerRequest();
      
            builder.Register(c => config.CreateMapper()).As<IMapper>().InstancePerRequest();

            builder.RegisterType<Logger>().As<ILogger>();
            base.Load(builder);
        }
    }
}
