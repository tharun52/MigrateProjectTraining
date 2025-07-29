using Microsoft.EntityFrameworkCore;
using ShoppingApp.Contexts;
using ShoppingApp.Models;

namespace ShoppingApp.Repositories
{
    public class RefreshTokenRepository: Repository<int, RefreshToken>
    {
        public RefreshTokenRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public override async Task<RefreshToken> Get(int key)
        {
            return await  _appDbContext.RefreshTokens
                .SingleOrDefaultAsync(rt => rt.Id == key)
                ?? throw new KeyNotFoundException($"Refresh token with ID {key} not found.");
        }

        public override async Task<IEnumerable<RefreshToken>> GetAll()
        {
            return await _appDbContext.RefreshTokens
                .Where(rt => !rt.IsRevoked)
                .ToListAsync();
        }
    }
}