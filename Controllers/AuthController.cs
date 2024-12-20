using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchMe.Data;
using WatchMe.Models;
using WatchMe.Dtos;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System;

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

        // E-posta kontrolü
        [HttpPost("check-email")]
        public async Task<IActionResult> CheckEmail([FromBody] EmailCheckRequest emailRequest)
        {
            var emailExists = await _context.Users.AnyAsync(u => u.Email == emailRequest.Email);
            return Ok(new { isEmailTaken = emailExists });
        }

        // Register (kayıt işlemi)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                // Email adresinin zaten kullanılıp kullanılmadığını kontrol et
                if (await _context.Users.AnyAsync(u => u.Email == userDto.Email))
                    return BadRequest("User with this email already exists");

                // Nickname zorunlu olmalı
                if (string.IsNullOrEmpty(userDto.Nickname))
                    return BadRequest("Nickname is required");

                // Şifreyi hash'le
                var hashedPassword = HashPassword(userDto.Password);

                // Kullanıcıyı veritabanına ekle (şifreyi hash'leyerek kaydediyoruz)
                var user = new User
                {
                    Nickname = userDto.Nickname,  // Nickname kayıt sırasında zorunlu
                    Email = userDto.Email,
                    Password = hashedPassword  // Hashlenmiş şifreyi veritabanına kaydediyoruz
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok("User registered successfully");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Login (giriş işlemi)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Invalid login data.");
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
                return Unauthorized("Invalid email or password");

            // Şifreyi doğrulama işlemi
            if (!VerifyPassword(user.Password, loginDto.Password))
                return Unauthorized("Invalid email or password");

            return Ok(new { message = "Login successful" });
        }

        // Şifreyi doğrulama metodu (girdiğimiz şifreyi hash'leyip kontrol ediyoruz)
        private bool VerifyPassword(string storedPassword, string enteredPassword)
        {
            var enteredPasswordHash = HashPassword(enteredPassword);
            return storedPassword == enteredPasswordHash;
        }

        // Şifreyi hash'leme metodu
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashedBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }

    // E-posta kontrolü için request model
    public class EmailCheckRequest
    {
        public required string Email { get; set; }  // required ekledik
    }
}