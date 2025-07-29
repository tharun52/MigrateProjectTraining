using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Interfaces;
using ShoppingApp.Models.DTOs;

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

        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            var result = await _authService.RegisterUserAsync(request);
            if (!result.Success) return BadRequest(result.Message);

            return Ok(result.Message);
        }
        [HttpPost("signup/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterDto request)
        {
            var result = await _authService.RegisterAdminAsync(request);
            if (!result.Success) return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var result = await _authService.LoginAsync(request.Username, request.Password);
            if (!result.Success) return Unauthorized(result.Message);

            return Ok(new
            {
                Token = result.AccessToken,
                RefreshToken = result.RefreshToken
            });
        }
    }
}