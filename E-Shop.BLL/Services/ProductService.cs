using E_Shop.BLL.DTOs;
using E_Shop.BLL.Infrastucture;
using E_Shop.BLL.Interfaces;
using E_Shop.DAL.Interfaces;
using E_Shop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace E_Shop.BLL.Services
{
    public class ProductService : IProductService
    {
        IUnitOfWork _db;

        /// <summary>
        /// Initialize new Product Service instance
        /// </summary>
        /// <param name="db">IUnitOfWork instance</param>
        public ProductService(IUnitOfWork db)
        {
            _db = db;
        }

        public OperationDetails AddNewProduct(ProductDTO productDTO)
        {
            try
            {
                if (productDTO == null)
                {
                    return new OperationDetails(false, "product is null", "");
                }
                foreach (var item in _db.ProductRepository.Get().ToList())
                {
                    if (item.Name == productDTO.Name && item.Size == productDTO.Size && item.Color == productDTO.Color
                  && item.Price == productDTO.Price && item.PhotoUrl == productDTO.PhotoUrl && item.Price == productDTO.Price)
                    {
                        return new OperationDetails(false, "Product with the same name, size, color, price already exist", "");
                    }
                }
                Product product = new Product()
                {
                    Category = productDTO.Category,
                    Color = productDTO.Color,
                    Date = DateTimeOffset.UtcNow,
                    Gender = (Gender)(int)productDTO.Gender,
                    Name = productDTO.Name,
                    PhotoUrl = productDTO.PhotoUrl,
                    Price = productDTO.Price,
                    ProductDetails = productDTO.ProductDetails,
                    Size = productDTO.Size,
                };

                _db.ProductRepository.Create(product);
                _db.Save();

                return new OperationDetails(true, "Operation was successfully completed", "");
            }
            catch (NullReferenceException e) 
            {
                return new OperationDetails(false, "Operation was failed", e.Message);
            }
        }

        public OperationDetails DeleteProduct(int id)
        {
            try
            {
                var product = _db.ProductRepository.FindById(id);
                _db.ProductRepository.Remove(product);
                _db.Save();
                return new OperationDetails(true, "Operation was successfully completed", "");
            }

            catch(NullReferenceException e)
            {
                return new OperationDetails(false, "", e.Message);
            }
        }

        public OperationDetails Edit(ProductDTO productDTO)
        {
            try
            {
                if (productDTO == null)
                {
                    return new OperationDetails(false, "productDTO is null", "");
                }
                var product = _db.ProductRepository.FindById(productDTO.Id);
                if (product == null)
                {
                    return new OperationDetails(false, "product is null", "");
                }

                product.Category = productDTO.Category;
                product.Color = productDTO.Color;
                product.Date = DateTimeOffset.UtcNow;
                product.Gender = (Gender)(int)productDTO.Gender;
                product.Name = productDTO.Name;
                product.PhotoUrl = productDTO.PhotoUrl;
                product.Price = productDTO.Price;
                product.ProductDetails = productDTO.ProductDetails;
                product.Size = productDTO.Size;

                _db.ProductRepository.Update(product);
                _db.Save();

                return new OperationDetails(true, "Operation was successfully completed", "");
            }
            catch(NullReferenceException e)
            {
                return new OperationDetails(false, "", e.Message);
            }
        }

        private IEnumerable<ProductDTO> ConvertProductsToProductsDTO(List<Product> products)
        {
            var productsDTO = new List<ProductDTO>();
            foreach (var item in products)
            {
                   var productDTO = new ProductDTO
                   {
                       Id = item.Id,
                       Category = item.Category,
                       Color = item.Color,
                       Date = item.Date,
                       Gender = (GenderDTO)(int)item.Gender,
                       Name = item.Name,
                       PhotoUrl = item.PhotoUrl,
                       Price = item.Price,
                       ProductDetails = item.ProductDetails,
                       Size=item.Size,
                   };

                   productsDTO.Add(productDTO);

            }

            var list = productsDTO.GroupBy(x=>new { x.Name,x.Color,x.Price}).Select(y => y.First()).ToList();
        
            return list;
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return ConvertProductsToProductsDTO(_db.ProductRepository.Get().ToList());
        }

        public IEnumerable<ProductDTO> GetAllProductsWithRepeats()
        {
            var productsDTO = new List<ProductDTO>();
            var products = _db.ProductRepository.Get().ToList();
            foreach (var item in products)
            {
                var productDTO = new ProductDTO
                {
                    Id = item.Id,
                    Category = item.Category,
                    Color = item.Color,
                    Date = item.Date,
                    Gender = (GenderDTO)(int)item.Gender,
                    Name = item.Name,
                    PhotoUrl = item.PhotoUrl,
                    Price = item.Price,
                    ProductDetails = item.ProductDetails,
                    Size = item.Size,
                };

                productsDTO.Add(productDTO);

            }
            return productsDTO;
        }

        public ProductDetailsDTO GetProductById(int id)
        {      
            var product = _db.ProductRepository.FindById(id);

            if (product == null)
            {
                return null;
            }
            var productList = _db.ProductRepository.Get(i=>i.Name==product.Name && i.Color==product.Color).ToList();

            List<IdSizeDTO> sizes = new List<IdSizeDTO>();

            foreach (var item in productList)
            {
                sizes.Add(new IdSizeDTO
                {
                   ProductId= item.Id,
                   Size=item.Size,
                });
            }
            ProductDetailsDTO productDTO = new ProductDetailsDTO
            {
                Id=product.Id,
                Category = product.Category,
                Color = product.Color,
                Name = product.Name,
                PhotoUrl = product.PhotoUrl,
                Price = product.Price,
                ProductDetails = product.ProductDetails,
                Sizes = sizes,
                Gender=(GenderDTO)(int)product.Gender,
             };
                return productDTO;            
        }

        public IEnumerable<ProductDTO> GetProductsByPrice(int min, int max, IEnumerable<ProductDTO> productDTOList)
        {     
            if(max==0)
            {
                return productDTOList.ToList();
            }
            return productDTOList.Where(i => i.Price > min && i.Price < max).ToList();
        }

        public IEnumerable<ProductDTO> GetProductsByCategory(string category, IEnumerable<ProductDTO> productDTOList)
        {
            if (String.IsNullOrWhiteSpace(category))
            {
                return productDTOList.ToList();
            }
            return productDTOList.Where(i => i.Category == category).ToList();   
        }

        public IEnumerable<ProductDTO> GetProductsByColor(string color, IEnumerable<ProductDTO> productDTOList)
        {
            if (String.IsNullOrWhiteSpace(color))
            {
                return productDTOList.ToList();
            }
            return productDTOList.Where(i => i.Color== color).ToList();
        }

        public IEnumerable<ProductDTO> GetProductsBySize(string size)
        {
            if (String.IsNullOrEmpty(size))
            {
                return GetAllProducts().ToList();
            }
            return GetAllProductsWithRepeats().Where(i => i.Size == size).ToList();
        }

        public IEnumerable<ProductDTO> GetProductsByName(string name, IEnumerable<ProductDTO> productDTOList)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                return productDTOList.ToList();
            }
            return productDTOList.Where(i => i.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public IEnumerable<ProductDTO> SortByParam(IEnumerable<ProductDTO> productDTOList,string param,bool descending)
        {
            var propertyInfo = typeof(ProductDTO).GetProperty(param);
            if (!descending)
            {
                return productDTOList.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
            }
            else
            {
                return productDTOList.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
            }

        }

        public SelectMenusDTO GetAllMenus()
        {
            var list = _db.ProductRepository.Get();
            var categoriesList = new HashSet<string>();
            var sizesList = new HashSet<string>();
            var colorsList = new HashSet<string>();

            foreach (var item in list)
            {
                categoriesList.Add(item.Category);
                sizesList.Add(item.Size);
                colorsList.Add(item.Color);
            }

            SelectMenusDTO dto = new SelectMenusDTO
            {
                Categories = categoriesList.OrderBy(i=>i),
                Colors=colorsList.OrderBy(i => i),
                Sizes=sizesList.OrderBy(i => i),
            };

            return dto;
        }

    }
}
