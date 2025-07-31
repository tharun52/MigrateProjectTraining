using System.Security.Claims;
using ShoppingApp.Interfaces;
using ShoppingApp.Interfaces.TrueVote.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Services
{
    public class NewsService : INewsService
    {
        private readonly IRepository<int, News> _newsRepository;
        private readonly IRepository<int, User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NewsService(IRepository<int, News> newsRepository,
                           IRepository<int, User> userRepository,
                           IHttpContextAccessor httpContextAccessor)
        {
            _newsRepository = newsRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<News> AddNews(AddNewsDto newsDto)
        {
            if (newsDto.Title == null)
            {
                throw new Exception("Tilte cannot be null");
            }
            if (newsDto.ShortDescription == null)
            {
                throw new Exception("Short Description cannot be null");
            }

            string imagePath = string.Empty;
            if (newsDto.Image != null && newsDto.Image.Length > 0)
            {
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                Directory.CreateDirectory(uploadPath);

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(newsDto.Image.FileName);
                string filePath = Path.Combine(uploadPath, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await newsDto.Image.CopyToAsync(fileStream);
                }

                imagePath = $"uploads/{uniqueFileName}";
            }

            if (newsDto.Content == null)
            {
                throw new Exception("Content cannot be null");
            }
            var loggedInUser = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = (await _userRepository.GetAll())
                .SingleOrDefault(u => u.Username == loggedInUser)
                ?? throw new Exception("No User Logged in") ;

            var news = new News
            {
                UserId = user.UserId,
                Title = newsDto.Title,
                ShortDescription = newsDto.ShortDescription,
                Image = imagePath,
                Content = newsDto.Content,
                CreatedDate = DateTime.UtcNow
            };

            return await _newsRepository.Add(news);
        }

        public async Task<IEnumerable<News>> GetAllNews()
        {
            var news = await _newsRepository.GetAll();
            if (news == null)
            {
                throw new Exception("No news exists in the db");
            }
            return news;
        }

        public async Task<News> GetNewsById(int id)
        {
            var news = await _newsRepository.Get(id);
            if (news == null)
            {
                throw new Exception("No news exists in the db");
            }
            return news;
        }

        public async Task<News> DeleteNewsById(int id)
        {
            var news = await _newsRepository.Delete(id);
            if (news == null)
            {
                throw new Exception("No news exists in the db");
            }
            return news;
        }
        public async Task<News> UpdateNews(int id, UpdateNewsDto newsDto)
        {
            var news = await _newsRepository.Get(id);
            if (news == null)
            {
                throw new Exception($"No news found with the id:{id}");
            }
            news.Title = newsDto.Title ?? news.Title;
            news.ShortDescription = newsDto.ShortDescription ?? news.ShortDescription;
            news.Content = newsDto.Content ?? news.Content;
            if (newsDto.Image != null && newsDto.Image.Length > 0)
            {
                string imagePath = string.Empty;
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                Directory.CreateDirectory(uploadPath);

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(newsDto.Image.FileName);
                string filePath = Path.Combine(uploadPath, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await newsDto.Image.CopyToAsync(fileStream);
                }

                imagePath = $"uploads/{uniqueFileName}";
                news.Image = imagePath;
            }
            return await _newsRepository.Update(id, news);
        }
    }
}