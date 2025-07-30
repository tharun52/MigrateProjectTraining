using System.Security.Claims;
using ShoppingApp.Interfaces;
using ShoppingApp.Interfaces.TrueVote.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<int, Product> _productRepository;
        private readonly IRepository<int, User> _userRepostiory;
        private readonly IRepository<int, Category> _categoryRepostiory;
        private readonly IRepository<int, Color> _colorRepostiory;
        private readonly IRepository<int, Model> _modelRepostiory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IRepository<int, Product> productRepository,
                              IRepository<int, User> userRepostiory,
                              IRepository<int, Category> categoryRepostiory,
                              IRepository<int, Color> colorRepostiory,
                              IRepository<int, Model> modelRepostiory,
                              IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _userRepostiory = userRepostiory;
            _categoryRepostiory = categoryRepostiory;
            _colorRepostiory = colorRepostiory;
            _modelRepostiory = modelRepostiory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Product> AddProduct(ProductAddDto productDto)
        {
            if (productDto.ProductName == null)
            {
                throw new Exception("Product Name cannot be null");
            }
            string imagePath = string.Empty;

            if (productDto.Image != null && productDto.Image.Length > 0)
            {
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                Directory.CreateDirectory(uploadPath);

                string originalFileName = Path.GetFileNameWithoutExtension(productDto.Image.FileName);
                string extension = Path.GetExtension(productDto.Image.FileName);
                string uniqueFileName = $"{originalFileName}_{Guid.NewGuid()}{extension}";

                string filePath = Path.Combine(uploadPath, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await productDto.Image.CopyToAsync(fileStream);
                }

                imagePath = $"uploads/{uniqueFileName}";
            }

            var loggedInUser = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = (await _userRepostiory.GetAll())
                .SingleOrDefault(u => u.Username == loggedInUser)
                ?? throw new Exception("No User Logged in") ;

            var userId = user.UserId;

            var newproduct = new Product
            {
                ProductName = productDto.ProductName,
                Image = imagePath,
                UserId = userId
            };

            if (productDto.CategoryId != null)
            {
                var category = await _categoryRepostiory.Get(productDto.CategoryId.Value)
                    ?? throw new Exception($"{productDto.CategoryId} is a wrong Category Id");

                newproduct.CategoryId = category.CategoryId;
            }

            if (productDto.ColorId != null)
            {
                var color = await _colorRepostiory.Get(productDto.ColorId.Value)
                    ?? throw new Exception($"{productDto.ColorId} is a wrong Color Id");

                newproduct.ColorId = color.ColorId;
            }

            if (productDto.ModelId != null)
            {
                var model = await _modelRepostiory.Get(productDto.ModelId.Value)
                    ?? throw new Exception($"{productDto.ModelId} is a wrong Model Id");

                newproduct.CategoryId = model.ModelId;
            }

            newproduct.Storage = productDto.Storage ?? null;
            newproduct.SellStartDate = productDto.SellStartDate ?? null;
            newproduct.SellEndDate = productDto.SellEndDate ?? null;

            if (productDto.IsNew == null)
            {
                newproduct.IsNew = 0;
            }
            else if (productDto.IsNew == 0 || productDto.IsNew == 1)
            {
                newproduct.IsNew = productDto.IsNew.Value;
            }
            else
            {
                throw new Exception("Product Dto IsNew should either be 0 or 1");
            }

            return await _productRepository.Add(newproduct);
        }
        public async Task<ProductGetDto?> GetProductById(int id)
        {
            var product = await _productRepository.Get(id);
            if (product == null)
                return null;

            var category = product.CategoryId != null
                ? (await _categoryRepostiory.Get(product.CategoryId.Value))?.Name
                : null;

            var color = product.ColorId != null
                ? (await _colorRepostiory.Get(product.ColorId.Value))?.Color1
                : null;

            var model = product.ModelId != null
                ? (await _modelRepostiory.Get(product.ModelId.Value))?.Model1
                : null;

            return new ProductGetDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Image = product.Image,
                Category = category,
                Color = color,
                Model = model,
                Storage = product.Storage,
                SellStartDate = product.SellStartDate,
                SellEndDate = product.SellEndDate,
                IsNew = product.IsNew
            };
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _productRepository.GetAll();
            if (products == null)
            {
                throw new Exception("No Products in the database.");
            }
            return products;
        }
        public async Task<Product> DeleteProduct(int id)
        {
            var product = await _productRepository.Get(id);
            if (product == null)
            {
                throw new Exception($"No product found with the id: {id}");
            }
             if (!string.IsNullOrEmpty(product.Image))
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), product.Image);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            return await _productRepository.Delete(id);
        }
    }
}