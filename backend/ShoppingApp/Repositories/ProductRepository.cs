using Microsoft.EntityFrameworkCore;
using ShoppingApp.Contexts;
using ShoppingApp.Models;

namespace ShoppingApp.Repositories
{
    public class ProductRepository : Repository<int, Product>
    {
        public ProductRepository(AppDbContext appDbContext): base(appDbContext)
        {}
        public override async Task<Product?> Get(int key)
        {
            return await _appDbContext.Products.SingleOrDefaultAsync(p => p.ProductId == key);
        }

        public override async Task<IEnumerable<Product>> GetAll()
        {
            return await _appDbContext.Products.ToListAsync();
        }
    }
}