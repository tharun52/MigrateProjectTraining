using Microsoft.EntityFrameworkCore;
using ShoppingApp.Contexts;
using ShoppingApp.Models;

namespace ShoppingApp.Repositories
{
    public class ColorRepository : Repository<int, Color>
    {
        public ColorRepository(AppDbContext appDbContext): base(appDbContext)
        {}
        public override async Task<Color?> Get(int key)
        {
            return await _appDbContext.Colors.SingleOrDefaultAsync(c => c.ColorId == key);
        }

        public override async Task<IEnumerable<Color>> GetAll()
        {
            return await _appDbContext.Colors.ToListAsync();
        }
    }
}