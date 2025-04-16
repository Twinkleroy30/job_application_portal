using Microsoft.AspNetCore.Mvc;
using JobPortalBackend.Models;
using JobPortalBackend.Data;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobPortalBackend.Controllers
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AppDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Email and password are required");
            }

            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return BadRequest("Email already exists");
            }

            user.Password = HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginUser)
        {
            _logger.LogInformation("Login attempt: {@LoginUser}", loginUser);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state invalid: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(loginUser.Email) || string.IsNullOrEmpty(loginUser.Password))
            {
                _logger.LogWarning("Email or password missing");
                return BadRequest("Email and password are required");
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginUser.Email);
            if (user == null)
            {
                _logger.LogWarning("User not found: {Email}", loginUser.Email);
                return Unauthorized("Invalid email or password");
            }

            var hashedPassword = HashPassword(loginUser.Password);
            if (user.Password != hashedPassword)
            {
                _logger.LogWarning("Invalid password for user: {Email}", loginUser.Email);
                return Unauthorized("Invalid email or password");
            }

            return Ok(new
            {
                user = new
                {
                    user.Id,
                    user.FullName,
                    user.Username,
                    user.Email,
                    user.Phone
                }
            });
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
