using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoppingApp.Contexts;
using ShoppingApp.Models;

namespace ShoppingApp.Repositories
{
    public class UserRepository : Repository<int, User>
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {}

        public override async Task<User?> Get(int UserId)
        {
            return await _appDbContext.Users.SingleOrDefaultAsync(u => u.UserId == UserId);
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _appDbContext.Users.ToListAsync();
        }
    }
}