using Microsoft.EntityFrameworkCore;
using ShoppingApp.Contexts;
using ShoppingApp.Models;

namespace ShoppingApp.Repositories
{
    public class NewsRepository : Repository<int, News>
    {
        public NewsRepository(AppDbContext appDbContext) : base(appDbContext)
        {}
        public override async Task<News?> Get(int key)
        {
            return await _appDbContext.News.SingleOrDefaultAsync(n => n.NewsId == key);
        }

        public override async Task<IEnumerable<News>> GetAll()
        {
            return await _appDbContext.News.ToListAsync();
        }
    }
}