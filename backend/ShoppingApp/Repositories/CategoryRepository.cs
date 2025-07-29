using Microsoft.EntityFrameworkCore;
using ShoppingApp.Contexts;
using ShoppingApp.Models;

namespace ShoppingApp.Repositories
{
    public class CategoryRepository : Repository<int, Category>
    {
        public CategoryRepository(AppDbContext appDbContext): base(appDbContext)
        {}
        public override async Task<Category?> Get(int key)
        {
            return await _appDbContext.Categories.SingleOrDefaultAsync(c => c.CategoryId == key);
        }

        public override async Task<IEnumerable<Category>> GetAll()
        {
            return await _appDbContext.Categories.ToListAsync();
        }
    }
}