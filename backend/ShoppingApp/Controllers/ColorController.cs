using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Interfaces;
using ShoppingApp.Models;

namespace ShoppingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateColorAsync(int id, [FromBody] string name)
        {
            var updatecolor = new Color { ColorId = id, Color1 = name };
            Console.WriteLine(updatecolor.Color1);
            var updatedcolor = await _colorService.EditColorAsync(updatecolor);
            if (updatedcolor == null)
            {
                throw new Exception($"Error in updating the color with the id{id}");
            }
            return Ok(updatedcolor);
        } 

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteColorAsync(int id)
        {
            var deleteColor = await _colorService.DeleteColorAsync(id);
            if (deleteColor == null)
            {
                throw new Exception($"Error in deleting the color with the id{id}");
            }
            return Ok(deleteColor);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateColorAsync([FromBody] string name)
        {
            var newcolor = await _colorService.AddColorAsync(name);
            return Ok(newcolor);
        }

        [HttpGet]
        public async Task<IActionResult> GetColorsAsync()
        {
            var colors = await _colorService.GetColorsAsync();
            if (colors == null)
            {
                throw new Exception("No colors found in the db");
            }
            return Ok(colors);
        } 
    }
}