using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Interfaces
{
    public interface INewsService
    {
        public Task<News> AddNews(AddNewsDto newsDto);
        public Task<IEnumerable<News>> GetAllNews();
        public Task<News> GetNewsById(int id);
        public Task<News> DeleteNewsById(int id);
        public Task<News> UpdateNews(int id, UpdateNewsDto newsDto);
    }
}