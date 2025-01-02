using Microsoft.AspNetCore.Mvc;
using WatchMe.Models;
using WatchMe.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace WatchMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Reset Password View
        public IActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid token.");
            }

            ViewData["Token"] = token; // Send token to the view
            return View();
        }

        // Privacy Page
        public IActionResult Privacy()
        {
            ViewData["ShowHeaderFooter"] = true;
            return View();
        }

        // Login Success View
        [HttpGet("LoginSuccess")]
        public IActionResult LoginSuccess()
        {
            return View(); // Returns LoginSuccess.cshtml
        }

        // Index - List all movies
        public IActionResult Index()
        {
            var allMovies = _context.Movies.ToList(); // Get all movies from the database
            return View(allMovies); // Send movies to the view
        }

        // Welcome Page (without header/footer)
        public IActionResult WelcomePage()
        {
            ViewData["ShowHeaderFooter"] = false;
            return View();
        }

        // Login Page (without header/footer)
        public IActionResult Login()
        {
            ViewData["ShowHeaderFooter"] = false;
            return View();
        }

        // Register Page (without header/footer)
        public IActionResult Register()
        {
            ViewData["ShowHeaderFooter"] = false;
            return View();
        }

        // Forgot Password Page (without header/footer)
        public IActionResult ForgotPassword()
        {
            ViewData["ShowHeaderFooter"] = false;
            return View();
        }

        // Profile Page (with header/footer)
        public IActionResult Profile()
        {
            ViewData["ShowHeaderFooter"] = true;
            return View();
        }

        // Netflix Page (list all movies)
        public IActionResult Netflix()
        {
            var allMovies = _context.Movies.ToList();
            return View(allMovies);
        }

<<<<<<< Updated upstream
=======
        // Display movies by genre
        public async Task<IActionResult> MoviesByGenre(int genreId)
        {
            var genre = await _context.Genres
                .Include(g => g.MovieGenres) // Including the relationship between Genre and MovieGenres
                .ThenInclude(mg => mg.Movie) // Including related movies for each genre
                .FirstOrDefaultAsync(g => g.GenreId == genreId);

            if (genre == null)
            {
                return NotFound("Genre not found.");
            }

            return View(genre); // Pass genre data to the view
        }
>>>>>>> Stashed changes
    }
}