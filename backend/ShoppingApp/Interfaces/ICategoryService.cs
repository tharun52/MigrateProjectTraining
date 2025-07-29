using ShoppingApp.Models;

namespace ShoppingApp.Interfaces
{
    public interface ICategoryService
    {
        public Task<Category> AddCategoryAsync(string name);
        public Task<IEnumerable<Category>> GetCategoriesAsync();
        public Task<Category> EditCategoryAsync(Category updatecategory);
        public Task<Category> DeleteCategoryAsync(int categoryId);
    }
}