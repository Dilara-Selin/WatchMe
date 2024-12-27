using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using WatchMe.Models;
using System.Linq;

namespace WatchMe.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        // Filmleri listele
        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        // Film detay sayfası
        public IActionResult Details(int id)
        {
            var movie = _context.Movies
                .Where(m => m.MovieId == id)
                .FirstOrDefault();

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
    }
}
