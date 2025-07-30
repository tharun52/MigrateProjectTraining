using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Interfaces;
using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNewsAsync()
        {
            var news = await _newsService.GetAllNews();
            if (news == null)
            {
                throw new Exception("No news in the database");
            }
            return Ok(news);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> GetNewsByIdAsync(int id)
        {
            var news = await _newsService.GetNewsById(id);
            if (news == null)
            {
                throw new Exception($"No news found with the id:{id}");
            }
            return Ok(news);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddNewsAsync([FromForm] AddNewsDto newsDto)
        {
            var news = await _newsService.AddNews(newsDto);
            if (news == null)
            {
                throw new Exception("Failled to add news");
            }
            return Ok(news);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateNewsAsync(int id, [FromForm] UpdateNewsDto newsDto)
        {
            var news = await _newsService.UpdateNews(id, newsDto);
            if (news == null)
            {
                throw new Exception($"Failed to update news with the id{id}");
            }
            return Ok(news);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNewsAsync(int id)
        {
            var news = await _newsService.DeleteNewsById(id);
            if (news == null)
            {
                throw new Exception($"Failed to delete news with the id{id}");
            }
            return Ok(news);
        }
    }
}