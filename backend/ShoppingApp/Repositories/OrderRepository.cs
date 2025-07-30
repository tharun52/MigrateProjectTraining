using Microsoft.EntityFrameworkCore;
using ShoppingApp.Contexts;
using ShoppingApp.Models;

namespace ShoppingApp.Repositories
{
    public class OrderRepository : Repository<int, Order>
    {
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {}
        public override async Task<Order?> Get(int key)
        {
            return await _appDbContext.Orders.SingleOrDefaultAsync(n => n.OrderID == key);
        }

        public override async Task<IEnumerable<Order>> GetAll()
        {
            return await _appDbContext.Orders.ToListAsync();
        }
    }
}