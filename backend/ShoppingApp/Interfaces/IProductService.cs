using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Interfaces
{
    public interface IProductService
    {
        public Task<Product> AddProduct(ProductAddDto productDto);
        public Task<ProductGetDto?> GetProductById(int id);
        public Task<IEnumerable<Product>> GetAllProducts();
        public Task<Product> DeleteProduct(int id);
    }
}