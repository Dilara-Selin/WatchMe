using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchMe.Data;
using WatchMe.Models;
using WatchMe.Dtos;  // DTO namespace'ini dahil et

namespace WatchMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            // Email adresinin zaten kullanılıp kullanılmadığını kontrol et
            if (await _context.Users.AnyAsync(u => u.Email == userDto.Email))
                return BadRequest("User with this email already exists");

            // Şifreyi ayarla ve User modeline dönüştür
            var user = new User
            {
                Nickname = userDto.Nickname,
                Email = userDto.Email,
                Password = userDto.Password  // Bu işlem şifre hashleme ve salt oluşturmayı tetikler
            };

            // Kullanıcıyı veritabanına ekle
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        // Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto loginDto)
        {
            // Kullanıcıyı email adresine göre bul
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null) 
                return Unauthorized("Invalid email or password");

            // Şifre doğrulama
            if (!user.VerifyPassword(loginDto.Password))
                return Unauthorized("Invalid email or password");

            return Ok("Login successful");
        }
    }
}