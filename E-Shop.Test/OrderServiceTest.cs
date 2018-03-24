using E_Shop.BLL.DTOs;
using E_Shop.BLL.Services;
using E_Shop.DAL.Interfaces;
using E_Shop.DAL.Models;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace E_Shop.Test
{
    [TestFixture]
    public class OrderServiceTest
    {
        Mock<IUserStore<Customer>> store;
        UserManager<Customer> userManager;
        Mock<IUnitOfWork> mock;
        OrderService service;


        [SetUp]
        public void SetUp()
        {
            mock = new Mock<IUnitOfWork>();
            service = new OrderService(mock.Object);
        }

        [Test]
        public void ChangeOrderStatus_OrderExist_ReturnOperationDetailsWithTrueValue()
        {
            Order order = new Order { OrderStatus = OrderStatus.Registered };
            mock.Setup(i => i.OrderRepository.FindById(It.IsAny<int>())).Returns(order);

            var result = service.ChangeOrderStatus(3, OrderStatusDTO.Paid);

            Assert.AreEqual(result.Succedeed, true);
        }

        [Test]
        public void ChangeOrderStatus_OrderDoesNotExist_ReturnOperationDetailsWithFalseValue()
        {
            mock.Setup(i => i.OrderRepository.FindById(It.IsAny<int>())).Returns((Order)null);

            var result = service.ChangeOrderStatus(3, OrderStatusDTO.Paid);

            Assert.AreEqual(result.Succedeed, false);
        }

        [Test]
        public void GetUsersOrders_UserDoesNotExist_ReturnNull()
        {
            store = new Mock<IUserStore<Customer>>(MockBehavior.Strict);
            store.As<IUserStore<Customer>>().Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((Customer)null);
            userManager = new UserManager<Customer>(store.Object);

            mock.Setup(i => i.UserManager).Returns(userManager);

            var result=service.GetAllUserOrders("qwdasdqwe");

            Assert.IsNull(result);
        }

        [Test]
        public void MakeOrder_UserDoesNotExist_ReturnOperationDetailsWithFalseValue()
        {
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            string json = @"[{'Id': 1,'Quantity': 2 }]";
            context.Setup(i => i.Request.Cookies).Returns(new HttpCookieCollection
            {
                new HttpCookie("Cart",json),
            });
            Mock<IIdentity> identity = new Mock<IIdentity>();
            identity.Setup(i => i.Name).Returns("name12300");
            context.Setup(i => i.User.Identity).Returns(identity.Object);
            store = new Mock<IUserStore<Customer>>(MockBehavior.Strict);
            store.As<IUserStore<Customer>>().Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((Customer)null);
            userManager = new UserManager<Customer>(store.Object);

            mock.Setup(i => i.UserManager).Returns(userManager);

            var result = service.MakeOrder(context.Object);

            Assert.AreEqual(result.Succedeed,false);
        }

        [Test]
        public void MakeOrder_UserExist_ReturnOperationDetailsWithTrueValue()
        {
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            string json = @"[{'Id': 1,'Quantity': 2 }]";
            context.Setup(i => i.Request.Cookies).Returns(new HttpCookieCollection
            {
                new HttpCookie("Cart",json),
            });
            context.Setup(i => i.Response.Cookies).Returns(new HttpCookieCollection
            {
                new HttpCookie("Cart",json),
            });
            Mock<IIdentity> identity = new Mock<IIdentity>();
            identity.Setup(i => i.Name).Returns("name12300");
            context.Setup(i => i.User.Identity).Returns(identity.Object);
            store = new Mock<IUserStore<Customer>>(MockBehavior.Strict);
            store.As<IUserStore<Customer>>().Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new Customer
            {
                Id="312",
                Address="a,",
                FirstName="a",
                LastName="a",
                UserName="aa",
                Email="123",
            });
            userManager = new UserManager<Customer>(store.Object);
            mock.Setup(i => i.ProductRepository.FindById(It.IsAny<int>())).Returns(new Product
            {
                Id = 3,
                Category = "a",
                Color = "a",
                Date = DateTimeOffset.Now,
                Gender = Gender.Men,
                Name = "a",
                PhotoUrl = "a",
                Price = 454,
                ProductDetails = "a",
                Size = "L",
            });
            Mock<IGenericRepository<Order>> orderMock = new Mock<IGenericRepository<Order>>();
            mock.Setup(i => i.OrderRepository).Returns(orderMock.Object);
            mock.Setup(i => i.UserManager).Returns(userManager);

            var result = service.MakeOrder(context.Object);

            Assert.AreEqual(result.Succedeed, true);
        }
    }
}
