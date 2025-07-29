using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Interfaces;
using ShoppingApp.Models;

namespace ShoppingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] string name)
        {
            var updatecategory = new Category { CategoryId = id, Name = name };
            Console.WriteLine(updatecategory.Name);
            var updatedcategory = await _categoryService.EditCategoryAsync(updatecategory);
            if (updatedcategory == null)
            {
                throw new Exception($"Error in updating the category with the id{id}");
            }
            return Ok(updatedcategory);
        } 

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var deleteCategory = await _categoryService.DeleteCategoryAsync(id);
            if (deleteCategory == null)
            {
                throw new Exception($"Error in deleting the category with the id{id}");
            }
            return Ok(deleteCategory);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] string name)
        {
            var newcategory = await _categoryService.AddCategoryAsync(name);
            return Ok(newcategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            if (categories == null)
            {
                throw new Exception("No categories found in the db");
            }
            return Ok(categories);
        } 
    }
}