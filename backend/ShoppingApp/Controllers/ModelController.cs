using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Interfaces;
using ShoppingApp.Models;

namespace ShoppingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteModelAsync(int id)
        {
            var deleteModel = await _modelService.DeleteModelAsync(id);
            if (deleteModel == null)
            {
                throw new Exception($"Error in deleting the model with the id{id}");
            }
            return Ok(deleteModel);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateModelAsync([FromBody] string name)
        {
            var newmodel = await _modelService.AddModelAsync(name);
            return Ok(newmodel);
        }

        [HttpGet]
        public async Task<IActionResult> GetModelsAsync()
        {
            var models = await _modelService.GetModelsAsync();
            if (models == null)
            {
                throw new Exception("No models found in the db");
            }
            return Ok(models);
        } 
    }
}