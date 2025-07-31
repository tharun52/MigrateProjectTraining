using ShoppingApp.Interfaces;
using ShoppingApp.Interfaces.TrueVote.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<int, User> _userRepository;
        private readonly IRepository<int, RefreshToken> _refreshTokenRepository;
        private readonly ITokenService _tokenService;
        private readonly string? _securityKey;

        public AuthService(IRepository<int, User> userRepository, IRepository<int, RefreshToken> refreshTokenRepository, ITokenService tokenService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
            _securityKey = configuration["Keys:AdminLoginKey"];
        }
        public async Task<(bool Success, string Message)> RegisterAdminAsync(AdminRegisterDto registerDto)
        {
            if (registerDto == null || string.IsNullOrWhiteSpace(registerDto.Username) || string.IsNullOrWhiteSpace(registerDto.Password))
                return (false, "Invalid input.");

            if (registerDto.SecretKey != _securityKey)
                return (false, "Invalid secret key.");

            var existingUser = (await _userRepository.GetAll())
                .FirstOrDefault(u => u.Username.Equals(registerDto.Username, StringComparison.OrdinalIgnoreCase));

            if (existingUser != null)
                return (false, "Username already exists.");

            var newUser = new User
            {
                Username = registerDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = "Admin"
            };

            await _userRepository.Add(newUser);
            return (true, "Admin registered successfully.");
        }

        public async Task<UserLoginResponse> RefreshLogin(string refreshToken)
        {
            var existingToken = (await _refreshTokenRepository.GetAll())
                .FirstOrDefault(r => r.Token == refreshToken && !r.IsRevoked && r.ExpiresAt > DateTime.UtcNow);

            if (existingToken == null)
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");

            var users = await _userRepository.GetAll();
            var user = users.SingleOrDefault(u => u.Username == existingToken.Username);
            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            existingToken.IsRevoked = true;
            await _refreshTokenRepository.Update(existingToken.Id, existingToken);

            var (accessToken, newRefreshToken) = await _tokenService.GenerateTokensAsync(user);

            return new UserLoginResponse
            {
                Username = user.Username,
                Token = accessToken,
                RefreshToken = newRefreshToken,
                Role = user.Role
            };
        }


        public async Task<(bool Success, string Message)> RegisterUserAsync(RegisterDto registerDto)
        {
            if (registerDto == null || string.IsNullOrWhiteSpace(registerDto.Username) || string.IsNullOrWhiteSpace(registerDto.Password))
                return (false, "Invalid input.");

            var existingUser = (await _userRepository.GetAll())
                .FirstOrDefault(u => u.Username.Equals(registerDto.Username, StringComparison.OrdinalIgnoreCase));

            if (existingUser != null)
                return (false, "Username already exists.");

            var newUser = new User
            {
                Username = registerDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = "User"
            };

            await _userRepository.Add(newUser);
            return (true, "User registered successfully.");
        }


        public async Task<UserLoginResponse> Login(UserLoginRequest user)
        {
            var dbusers = await _userRepository.GetAll();
            var dbUser = dbusers.SingleOrDefault(u => u.Username == user.Username);
            if (dbUser == null)
            {
                throw new Exception("No such user");
            }
            if (!BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
            {
                throw new Exception("Invalid password");
            }
            var (accessToken, refreshToken) = await _tokenService.GenerateTokensAsync(dbUser);
            return new UserLoginResponse
            {
                Username = user.Username,
                Token = accessToken,
                RefreshToken = refreshToken,
                Role = dbUser.Role
            };
        }
        public async Task<bool> LogoutAsync(string refreshToken)
        {
            var token = (await _refreshTokenRepository.GetAll())
                .FirstOrDefault(r => r.Token == refreshToken && !r.IsRevoked);

            if (token == null)
                return false;

            token.IsRevoked = true;
            await _refreshTokenRepository.Update(token.Id, token);
            return true;
        }
        
    }
}