using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Interfaces;

namespace OrderManagement.API.Controllers
{
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // DEMO USERS – for real world use DB/Identity
            var users = new[]
            {
                new { Username = "admin", Password = "Admin123!", Role = "Admin" },
                new { Username = "user", Password = "User123!", Role = "User" }
            };

            var user = users.FirstOrDefault(u =>
                u.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase)
                && u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var token = _jwtTokenService.GenerateToken(user.Username, user.Role);

            return Ok(new LoginResponse
            {
                Token = token,
                Role = user.Role
            });
        }
    }
}
