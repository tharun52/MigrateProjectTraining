using Microsoft.EntityFrameworkCore;
using ShoppingApp.Contexts;
using ShoppingApp.Models;

namespace ShoppingApp.Repositories
{
    public class OrderDetailRepository : Repository<int, OrderDetail>
    {
        public OrderDetailRepository(AppDbContext appDbContext) : base(appDbContext)
        {}
        public override async Task<OrderDetail?> Get(int key)
        {
            return await _appDbContext.OrderDetails.SingleOrDefaultAsync(n => n.OrderID == key);
        }

        public override async Task<IEnumerable<OrderDetail>> GetAll()
        {
            return await _appDbContext.OrderDetails.ToListAsync();
        }
    }
}