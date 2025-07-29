using Microsoft.EntityFrameworkCore;
using ShoppingApp.Contexts;
using ShoppingApp.Models;

namespace ShoppingApp.Repositories
{
    public class ModelRepository : Repository<int, Model>
    {
        public ModelRepository(AppDbContext appDbContext): base(appDbContext)
        {}
        public override async Task<Model?> Get(int key)
        {
            return await _appDbContext.Models.SingleOrDefaultAsync(c => c.ModelId == key);
        }

        public override async Task<IEnumerable<Model>> GetAll()
        {
            return await _appDbContext.Models.ToListAsync();
        }
    }
}