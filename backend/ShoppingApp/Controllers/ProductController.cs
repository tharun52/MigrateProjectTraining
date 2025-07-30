using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productService.GetAllProducts();
            if (products == null)
            {
                throw new Exception("No products found in the db");
            }
            return Ok(products);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                throw new Exception($"No product with the Id:{id}");
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromForm] ProductAddDto productAddDto)
        {
            var product = await _productService.AddProduct(productAddDto);
            if (product == null)
            {
                throw new Exception("Failed to add the product");
            }
            return Ok(product);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var deletedProduct = await _productService.DeleteProduct(id);
            if (deletedProduct == null)
            {
                throw new Exception($"Failed to delete the product with id:{id}");
            }
            return Ok(deletedProduct);
        }
    }
}