using Microsoft.AspNetCore.Mvc;
using GreenNewJobs.Infrastructure.Services;

namespace GreenNewJobs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var user = GetUserFromDatabase(request.Username, request.Password, cancellationToken);

            if (user == null)
                return Unauthorized();

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }

        private User GetUserFromDatabase(string username, string password, CancellationToken cancellationToken)
        {
            if (username == "admin" && password == "password")
            {
                return new User { Id = Guid.NewGuid(), Username = "admin", Role = "Admin" };
            }
            if (username == "deliverer" && password == "password")
            {
                return new User { Id = Guid.NewGuid(), Username = "deliverer", Role = "Deliverer" };
            }
            return null;
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
