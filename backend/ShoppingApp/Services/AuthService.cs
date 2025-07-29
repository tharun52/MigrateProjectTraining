using ShoppingApp.Interfaces;
using ShoppingApp.Interfaces.TrueVote.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<int, User> _userRepository;
        private readonly ITokenService _tokenService;
        private readonly string? _securityKey;

        public AuthService(IRepository<int, User> userRepository, ITokenService tokenService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _securityKey = configuration["Keys:AdminLoginKey"];
        }
        public async Task<(bool Success, string Message)> RegisterAdminAsync(AdminRegisterDto registerDto)
        {
            var existingUser = (await _userRepository.GetAll()).FirstOrDefault(u => u.Username == registerDto.Username);
            if (existingUser != null)
                return (false, "Username already exists.");
            if (registerDto.SecretKey != _securityKey)
            {
                throw new Exception("Login failed, Wrong Secret Key");
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var newUser = new User
            {
                Username = registerDto.Username,
                Password = hashedPassword,
                Role = "Admin"
            };

            await _userRepository.Add(newUser);
            return (true, "User registered successfully.");
        }

        public async Task<(bool Success, string Message)> RegisterUserAsync(RegisterDto registerDto)
        {
            var existingUser = (await _userRepository.GetAll()).FirstOrDefault(u => u.Username == registerDto.Username);
            if (existingUser != null)
                return (false, "Username already exists.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var newUser = new User
            {
                Username = registerDto.Username,
                Password = hashedPassword,
                Role = "User"
            };

            await _userRepository.Add(newUser);
            return (true, "User registered successfully.");
        }

        public async Task<(bool Success, string Message, string? AccessToken, string? RefreshToken)> LoginAsync(string username, string password)
        {
            var user = (await _userRepository.GetAll()).FirstOrDefault(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return (false, "Invalid username or password.", null, null);
            }

            var (accessToken, refreshToken) = await _tokenService.GenerateTokensAsync(user);
            return (true, "Login successful.", accessToken, refreshToken);
        }
    }
}