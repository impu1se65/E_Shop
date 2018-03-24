using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using E_Shop.DAL.Interfaces;
using E_Shop.BLL.Services;
using E_Shop.DAL.Models;
using E_Shop.BLL.Infrastucture;
using E_Shop.BLL.DTOs;

namespace E_Shop.Test
{
    [TestFixture]
    public class ProductServiceTest
    {
        Mock<IGenericRepository<Product>> repositoryMock;
        Mock<IUnitOfWork> mock;
        ProductService service;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IGenericRepository<Product>>();
            mock = new Mock<IUnitOfWork>();
            service = new ProductService(mock.Object);
        }

        [Test]
        public void GetAllProducts_RepositoryDoesNotContainItems_ReturnEmptyCollection()
        {
            mock.Setup(i => i.ProductRepository.Get()).Returns(new List<Product>().AsQueryable());

            var result = service.GetAllProducts().ToList();

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetAllProducts_RepositoryContainsItems_ReturnCollectionWithOneElement()
        {
            var list = new List<Product>()
            {
                 new Product
                {
                    Id=1,
                    Category="a",
                    Color="a",
                    Date=DateTimeOffset.Now,
                    Gender=Gender.Men,
                    Name="a",
                    PhotoUrl="a",
                    Price=454,
                    ProductDetails="a",
                    Size="L",
                }
            };
            mock.Setup(i => i.ProductRepository.Get()).Returns(list.AsQueryable());

            var result = service.GetAllProducts().ToList();

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetAllProducts_RepositoryContainsItems_ReturnCollectionWithThreeItems()
        {
            var list = new List<Product>()
            {
                 new Product
                {
                    Id=1,
                    Category="a",
                    Color="c",
                    Date=DateTimeOffset.Now,
                    Gender=Gender.Men,
                    Name="b",
                    PhotoUrl="a",
                    Price=454,
                    ProductDetails="a",
                    Size="L",
                },
                    new Product
                {
                    Id=2,
                    Category="a",
                    Color="b",
                    Date=DateTimeOffset.Now,
                    Gender=Gender.Men,
                    Name="b",
                    PhotoUrl="a",
                    Price=454,
                    ProductDetails="a",
                    Size="M",
                },
                new Product
                {
                    Id=3,
                    Category="a",
                    Color="a",
                    Date=DateTimeOffset.Now,
                    Gender=Gender.Men,
                    Name="b",
                    PhotoUrl="a",
                    Price=454,
                    ProductDetails="a",
                    Size="M",
                }
            };
            mock.Setup(i => i.ProductRepository.Get()).Returns(list.AsQueryable());

            var result = service.GetAllProducts().ToList();

            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void AddNewProduct_ProductDtoNull_ReturnOperationDetailsWithFalseValue()
        {

            repositoryMock.Setup(i => i.Create(new Product()));
            mock.Setup(i => i.ProductRepository).Returns(repositoryMock.Object);
            var result = service.AddNewProduct(null);

            Assert.AreEqual(result.Succedeed,new OperationDetails(false,"","").Succedeed);
        }

        [Test]
        public void AddNewProduct_RepositoryExist_ReturnOperationDetailsWithTrueValue()
        {
            repositoryMock.Setup(i => i.Create(new Product()));
            mock.Setup(i => i.ProductRepository).Returns(repositoryMock.Object);
          var product= new ProductDTO
            {
                Category = "n",
                Color = "n",
                Date = DateTimeOffset.Now,
                Gender = GenderDTO.Men,
                Name = "n",
                PhotoUrl = "n",
                Price = 454,
                ProductDetails = "n",
                Size = "L",
            };
            var result = service.AddNewProduct(product);

            Assert.AreEqual(result.Succedeed, new OperationDetails(true, "", "").Succedeed);
        }


        [Test]
        public void DeleteProductWhenItExists_RepositoryExist_ReturnOperationDetailsWithTrueValue()
        {
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
            repositoryMock.Setup(i => i.Remove(product));
            mock.Setup(i => i.ProductRepository).Returns(repositoryMock.Object);
            var result = service.DeleteProduct(1);

            Assert.AreEqual(result.Succedeed, new OperationDetails(true, "", "").Succedeed);
        }

        [Test]
        public void EditProduct_DTOIsNotEmpty_ReturnOperationDetailsWithTrurValue()
        {
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
            repositoryMock.Setup(i => i.Update(product));
            repositoryMock.Setup(i=>i.FindById(1)).Returns(product);
            mock.Setup(i => i.ProductRepository).Returns(repositoryMock.Object);

            var result = service.Edit(new ProductDTO() {
                Id =1,
                Category = "n",
                Color = "n",
                Date = DateTimeOffset.Now,
                Gender = GenderDTO.Men,
                Name = "n",
                PhotoUrl = "n",
                Price = 454,
                ProductDetails = "n",
                Size = "L",
            });

            Assert.AreEqual(result.Succedeed, new OperationDetails(true, "", "").Succedeed);
        }

        [Test]
        public void GetProductById_ProductDoesNotExist_ReturnNull()
        {
            mock.Setup(i => i.ProductRepository.FindById(It.IsAny<int>())).Returns((Product)null);
            var result= service.GetProductById(3);
            Assert.IsNull(result);
        }

        [Test]
        public void GetProductById_ProductExist_ReturnProductDTO()
        {

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

            var productDTO = new ProductDetailsDTO
            {
                Category = "a",
                Color = "a",
                Name = "a",
                PhotoUrl = "a",
                Price = 454,
                ProductDetails = "a",
            };
            var result = service.GetProductById(3);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, productDTO.Name);
        }
       
    }
}
