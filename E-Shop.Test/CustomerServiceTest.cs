using E_Shop.BLL.DTOs;
using E_Shop.BLL.Infrastucture;
using E_Shop.BLL.Services;
using E_Shop.DAL.Identity;
using E_Shop.DAL.Interfaces;
using E_Shop.DAL.Models;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace E_Shop.Test
{
    [TestFixture]
    public class CustomerServiceTest
    {
       Mock<IUserStore<Customer>> store;
       UserManager<Customer> userManager;
       Mock<IUnitOfWork> mock;
       CustomerService service;


        [SetUp]
        public void SetUp()
        {        
            mock = new Mock<IUnitOfWork>();
            service = new CustomerService(mock.Object);
        }

        [Test]
        public void BanCustomer_UserIsExist_ReturnOperationDetailsWithTrueValue()
        {
            Customer customer = new Customer
            {
                UserName = "test@email.com",
                Email = "test@email.com",
            };
            store = new Mock<IUserStore<Customer>>(MockBehavior.Strict);
            store.As<IUserStore<Customer>>().Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(customer);
            userManager = new UserManager<Customer>(store.Object);

            mock.Setup(i => i.UserManager).Returns(userManager);

            var result = service.BanCustomer("dddadsasd");

            Assert.AreEqual(result.Succedeed, new OperationDetails(true, "", "").Succedeed);
        }

        [Test]
        public void CreateNewCustomer_EmailAlreadyUsed_ReturnOperationDetailsWithFalseValue()
        {
            Customer customer = new Customer
            {
                UserName = "test@email.com",
                Email = "test@email.com",
            };

            store = new Mock<IUserStore<Customer>>(MockBehavior.Strict);
            store.As<IUserEmailStore<Customer>>().Setup(x=>x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(customer);
            userManager = new UserManager<Customer>(store.Object);

            mock.Setup(i => i.UserManager).Returns(userManager);

            var result = service.Create(new CustomerDTO { Email="321321",});

            Assert.AreEqual(result.Result.Succedeed, new OperationDetails(false, "", "").Succedeed);
        }


        [Test]
        public void AuthenticateCustomer_UserIsExist_ReturnClaim()
        {
            Customer customer = new Customer
            {
                UserName = "test@email.com",
                Email = "test@email.com",
            };
            //(class)null
            store = new Mock<IUserStore<Customer>>(MockBehavior.Strict);
            store.As<IUserLoginStore<Customer>>().Setup(x => x.FindAsync(It.IsAny<UserLoginInfo>())).ReturnsAsync(customer);
            userManager = new UserManager<Customer>(store.Object);

            mock.Setup(i => i.UserManager).Returns(userManager);

            var result = service.Authenticate(new CustomerDTO { Email = "321321", Password="1233123"});

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetUserCart_ZeroProductsInShoppingCart_ReturnCartDTOWithZeroTotalPriceAndProductCollectionWithZeroItems()
        {           
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            context.Setup(i => i.Request.Cookies).Returns(new HttpCookieCollection
            {
                new HttpCookie("cookie"),
                new HttpCookie("cookie1"),
            });

            var result=service.GetUserCart(context.Object);

            Assert.AreEqual(result.totalPrice,   0 );
        }

        [Test]
        public void AddProductToCart_ZeroValues_ReturnOperationDetailsWithFailValue()
        {
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            context.Setup(i => i.Request.Cookies).Returns(new HttpCookieCollection
            {
                new HttpCookie("cookie"),
                new HttpCookie("cookie1"),
            });

            var result = service.AddProductToCart(new SerializeProductInfoForCart(),context.Object);

            Assert.AreEqual(result.Succedeed,false);
        }

        [Test]
        public void AddProductToCart_ReturnOperationDetailsWithTrueValue()
        {
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            context.Setup(i => i.Request.Cookies).Returns(new HttpCookieCollection
            {
                new HttpCookie("cookie"),
                new HttpCookie("cookie1"),
            });
            var response = new Mock<HttpResponseBase>();
            response.Setup(i => i.Cookies).Returns(new HttpCookieCollection
            {
                new HttpCookie("cookie"),
                new HttpCookie("cookie1"),
            });

            context.Setup(i=>i.Response).Returns(response.Object);
            var result = service.AddProductToCart(new SerializeProductInfoForCart {Id=1,Quantity=2 }, context.Object);

            Assert.AreEqual(result.Succedeed, true);
        }

        [Test]
        public void AddProductToCart_ProductAlreadyExistInCart()
        {
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            string json = @"[{'Id': 1,'Quantity': 2 }]";
            context.Setup(i => i.Request.Cookies).Returns(new HttpCookieCollection
            {
                new HttpCookie("Cart",json),
            });
            var response = new Mock<HttpResponseBase>();
            response.Setup(i => i.Cookies).Returns(new HttpCookieCollection
            {
                new HttpCookie("cookie"),
                new HttpCookie("cookie1"),
            });
       
            context.Setup(i => i.Response).Returns(response.Object);
            var result = service.AddProductToCart(new SerializeProductInfoForCart { Id = 1, Quantity = 2 }, context.Object);

            Assert.AreEqual(result.Succedeed, true);
        }
        [Test]
        public void RemoveProductFromCart_ItemExistInCart_ReturnOperationDetailsWithTrueValue()
        {
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            string json = @"[{'Id': 1,'Quantity': 2 }]";
            context.Setup(i => i.Request.Cookies).Returns(new HttpCookieCollection
            {
                new HttpCookie("Cart",json),
            });
            var response = new Mock<HttpResponseBase>();
            response.Setup(i => i.Cookies).Returns(new HttpCookieCollection
            {
                new HttpCookie("cookie"),
                new HttpCookie("cookie1"),
            });

            context.Setup(i => i.Response).Returns(response.Object);
            var result = service.RemoveProductFromCart(1, context.Object);

            Assert.AreEqual(result.Succedeed,true);
        }
        [Test]
        public void GetUserCart_ReturnProductsAndTotalPrice()
        {
            string json = @"[{'Id': 1,'Quantity': 2 }]";

            Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            context.Setup(i => i.Request.Cookies).Returns(new HttpCookieCollection
            {
                new HttpCookie("Cart",json),
            });

            var product = new Product
            {
                Id = 1,
                Category = "a",
                Color = "a",
                Date = DateTimeOffset.Now,
                Gender = Gender.Men,
                Name = "a",
                PhotoUrl = "a",
                Price = 454,
                ProductDetails = "a",
                Size = "L",
            };
            mock.Setup(i => i.ProductRepository.FindById(It.IsAny<int>())).Returns(product);
            var result = service.GetUserCart(context.Object);

            Assert.AreEqual(result.totalPrice, product.Price*2);
        }
    }
}
