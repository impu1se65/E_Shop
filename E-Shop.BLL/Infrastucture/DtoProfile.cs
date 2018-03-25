using AutoMapper;
using E_Shop.BLL.DTOs;
using E_Shop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.BLL.Infrastucture
{
    public class DtoProfile:Profile
    {
        public DtoProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<Customer, CustomerDTO>();
        }
    }
}
