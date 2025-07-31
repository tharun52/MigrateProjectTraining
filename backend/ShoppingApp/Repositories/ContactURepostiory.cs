using Microsoft.EntityFrameworkCore;
using ShoppingApp.Contexts;
using ShoppingApp.Models;

namespace ShoppingApp.Repositories
{
    public class ContactURepostiory : Repository<int, ContactU>
    {
        public ContactURepostiory(AppDbContext appDbContext) : base(appDbContext)
        { }

        public override async Task<ContactU?> Get(int key)
        {
            return await _appDbContext.ContactUs.SingleOrDefaultAsync(c => c.Id == key);
        }

        public override async Task<IEnumerable<ContactU>> GetAll()
        {
            return await _appDbContext.ContactUs.ToListAsync();
        }
    }
}