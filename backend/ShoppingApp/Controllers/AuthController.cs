using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Interfaces;
using ShoppingApp.Models.DTOs;
using System.Threading.Tasks;

namespace ShoppingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/Auth/register-user
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            var (success, message) = await _authService.RegisterUserAsync(registerDto);
            if (!success) return BadRequest(new { message });
            return Ok(new { message });
        }

        // POST: api/Auth/register-admin
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterDto registerDto)
        {
            var (success, message) = await _authService.RegisterAdminAsync(registerDto);
            if (!success) return BadRequest(new { message });
            return Ok(new { message });
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest loginDto)
        {
            try
            {
                var response = await _authService.Login(loginDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        // POST: api/Auth/refresh
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            try
            {
                var response = await _authService.RefreshLogin(refreshToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        // POST: api/Auth/logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            var result = await _authService.LogoutAsync(refreshToken);
            if (!result) return BadRequest(new { message = "Invalid refresh token" });
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
