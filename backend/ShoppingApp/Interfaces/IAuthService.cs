using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Interfaces
{
    public interface IAuthService
    {
        public Task<(bool Success, string Message)> RegisterAdminAsync(AdminRegisterDto registerDto);
        public Task<UserLoginResponse> RefreshLogin(string refreshToken);
        public Task<(bool Success, string Message)> RegisterUserAsync(RegisterDto registerDto);
        public Task<UserLoginResponse> Login(UserLoginRequest user);
        public Task<bool> LogoutAsync(string refreshToken);
    }
}