using ShoppingApp.Interfaces;
using ShoppingApp.Interfaces.TrueVote.Interfaces;
using ShoppingApp.Models;

namespace ShoppingApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<int, Category> _categoryRepository;

        public CategoryService(IRepository<int, Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Category> AddCategoryAsync(string name)
        {
            var categories = await _categoryRepository.GetAll();
            if (categories.SingleOrDefault(c => c.Name == name) != null)
            {
                throw new Exception("Category Already Exists");
            }
            var newcategory = new Category { Name = name };
            return await _categoryRepository.Add(newcategory);
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAll();
            if (categories == null)
            {
                throw new Exception("No categories in the db");
            }
            return categories.OrderBy(c => c.Name);
        }
        public async Task<Category> EditCategoryAsync(Category updatecategory)
        {
            var category = await _categoryRepository.Get(updatecategory.CategoryId);
            if (category == null)
            {
                throw new Exception($"No category exists with the Id: {updatecategory.CategoryId}");
            }

            category.Name = updatecategory.Name;

            return await _categoryRepository.Update(updatecategory.CategoryId, category);
        }

        public async Task<Category> DeleteCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.Get(categoryId);
            if (category == null)
            {
                throw new Exception($"No category exists with the Id : {categoryId}");
            }
            return await _categoryRepository.Delete(categoryId);
        }
    }
}