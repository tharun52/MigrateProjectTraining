using System.Security.Claims;
using ShoppingApp.Interfaces;
using ShoppingApp.Interfaces.TrueVote.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Services
{
    public class ContactUService : IContactUService
    {
        private readonly IRepository<int, ContactU> _contactUsRepository;
        private readonly IRepository<int, User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContactUService(IRepository<int, ContactU> contactUsRepository,
                               IRepository<int, User> userRepository,
                               IHttpContextAccessor httpContextAccessor)
        {
            _contactUsRepository = contactUsRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ContactU> AddContactU(ContactURequestDto Dto)
        {
            var loggedInUser = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = (await _userRepository.GetAll())
                .SingleOrDefault(u => u.Username == loggedInUser)
                ?? throw new Exception("No User Logged in");
            var newContactU = new ContactU
            {
                UserId = user.UserId,
                Name = Dto.Name,
                Email = Dto.Email,
                Phone = Dto.Phone,
                Content = Dto.Content
            };
            return await _contactUsRepository.Add(newContactU);
        }

        public async Task<bool> DeleteContactUsById(int id)
        {
            var loggedInUser = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = (await _userRepository.GetAll())
                .SingleOrDefault(u => u.Username == loggedInUser)
                ?? throw new Exception("No User Logged in");
            if (user.Role != "Admin")
            {
                throw new UnauthorizedAccessException("Only admin can delete");
            }
            await _contactUsRepository.Delete(id);
            return true;
        }
    }   
}