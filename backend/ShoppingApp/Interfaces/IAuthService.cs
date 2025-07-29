using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Interfaces
{
    public interface IAuthService
    {
        public Task<(bool Success, string Message)> RegisterUserAsync(RegisterDto registerDto);
        public Task<(bool Success, string Message)> RegisterAdminAsync(AdminRegisterDto registerDto);
        public Task<(bool Success, string Message, string? AccessToken, string? RefreshToken)> LoginAsync(string username, string password);
    }
}