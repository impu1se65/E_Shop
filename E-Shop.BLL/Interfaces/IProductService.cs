using E_Shop.BLL.DTOs;
using E_Shop.BLL.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.BLL.Interfaces
{
    /// <summary>
    /// Separates presentation layer from data access layer and imposes business rules for products 
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Add new product 
        /// </summary>
        /// <param name="productDTO">Product data transfer object</param>
        /// <returns>Operation details instance with result of operation</returns>
        OperationDetails AddNewProduct(ProductDTO productDTO);

        /// <summary>
        /// Delete product 
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Operation details instance with result of operation</returns>
        OperationDetails DeleteProduct(int id);

        /// <summary>
        /// Update information about product
        /// </summary>
        /// <param name="id">Product data transfer object</param>
        /// <returns>Operation details instance with result of operation</returns>
        OperationDetails Edit(ProductDTO productDTO);

        /// <summary>
        /// Return all products without repeats(Repeats = color and name are identical)
        /// </summary>
        /// <returns>Collection with data transfer objects</returns>
        IEnumerable<ProductDTO> GetAllProducts();

        /// <summary>
        /// Return all products with repeats(Repeats=Color and name are identical)
        /// </summary>
        /// <returns>Collection with data transfer objects</returns>
        IEnumerable<ProductDTO> GetAllProductsWithRepeats();

        /// <summary>
        /// Find product by id 
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product transfer object</returns>
        ProductDetailsDTO GetProductById(int id);

        /// <summary>
        /// Sort collection of productDTO by parameter
        /// </summary>
        /// <param name="productDTOList">Collection what need sort</param>
        /// <param name="param">Sort parameter (Product DTO property) </param>
        /// <param name="descending">Sort order</param>
        /// <returns>Collection with sorting data transfer objects</returns>
        IEnumerable<ProductDTO> SortByParam(IEnumerable<ProductDTO> productDTOList,string param, bool descending);

        /// <summary>
        /// Select products by category
        /// </summary>
        /// <param name="category">Product category</param>
        /// <param name="productDTOList">Сollection from which will be select items</param>
        /// <returns>Collection with data transfer objects</returns>
        IEnumerable<ProductDTO> GetProductsByCategory(string category,IEnumerable<ProductDTO> productDTOList);

        /// <summary>
        /// Select products by color
        /// </summary>
        /// <param name="color">Product color</param>
        /// <param name="productDTOList">Сollection from which will be select items</param>
        /// <returns>Collection with data transfer objects</returns>
        IEnumerable<ProductDTO> GetProductsByColor(string color, IEnumerable<ProductDTO> productDTOList);

        /// <summary>
        /// Select products by name
        /// </summary>
        /// <param name="name">Product name</param>
        /// <param name="productDTOList">Сollection from which will be select items</param>
        /// <returns>Collection with data transfer objects</returns>
        IEnumerable<ProductDTO> GetProductsByName(string name, IEnumerable<ProductDTO> productDTOList);

        /// <summary>
        /// Select products by min max price
        /// </summary>
        /// <param name="min">min price</param>
        /// <param name="max">max price</param>
        /// <param name="productDTOList">Сollection from which will be select items</param>
        /// <returns>Collection with data transfer objects</returns>
        IEnumerable<ProductDTO> GetProductsByPrice(int min, int max, IEnumerable<ProductDTO> productDTOList);

        /// <summary>
        /// Select products by size
        /// </summary>
        /// <param name="size">Product size</param>
        /// <param name="productDTOList">Сollection from which will be select items</param>
        /// <returns>Collection with data transfer objects</returns>
        IEnumerable<ProductDTO> GetProductsBySize(string size);

        /// <summary>
        /// Return all product`s categories,colors,sizes without repeats
        /// </summary>
        /// <returns>Select menus DTO with color,sizes,categories collections</returns>
        SelectMenusDTO GetAllMenus();

    }
}
