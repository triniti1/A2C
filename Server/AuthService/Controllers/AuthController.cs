using A2C.CRM.Api.Data;
using A2C.CRM.Api.DTOs;
using A2C.CRM.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;



namespace A2C.CRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            // שלוף את כל המשתמשים עם UserRole
            var users = await _context.Users
                .Include(u => u.UserRole)
                .AsNoTracking() // מומלץ כשאין צורך בשינוי הנתונים
                .ToListAsync();

            // המר ל-DTO עם Role כמחרוזת
            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                CreatedAt = u.CreatedAt,
                Role = u.UserRole?.RoleName.ToString() ?? "Unknown"
            }).ToList();

            return Ok(userDtos);



           // return Ok(users);
        }

        /*[HttpPost("delete_user")]
        public IActionResult DeleteUser([FromBody] LoginRequest request)
        {
            return Ok(new { user.Id, user.Name, user.Email });
        }*/

            // 1. Check if user exists
            // 2. Password authontication
            // 3. Generate JWT token
            [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest request)
        {
            var user = _context.Users
                .Include(u => u.UserRole) // Include UserRole to access RoleName
                .SingleOrDefault(u => u.Email == request.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            if (user.UserRole == null)
                return Unauthorized("Invalid User role");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        private string GenerateJwtToken(User user)
        {
            // Create claims based on user information
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.RoleName.ToString())
            };
            // Generate a JWT token
            // Using secret key to signing the token credentials that exist only on the server
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            // Create signing credentials using the key and algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token with issuer, audience, claims, expiration time, and signing credentials
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // 1. Check if user with this email already exists
        // 2. Create a new user
        // 3. Hash the password using PasswordHasher
        // 4. Add the user to the database
        // 5. Return user information
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest("User with this email already exists.");

            // Create a new user
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow,
                UserRoleId = Guid.NewGuid(), // This should be set based on the role type
                UserRole = new UserRole { RoleName = request.Role ?? RoleType.User } // Default to User if not specified
            };

            // Hash the password using PasswordHasher
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { user.Id, user.Name, user.Email });
        }


        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
