using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using WatchMe.Models;
using System.Linq;

namespace WatchMe.Controllers
{
    public class GenreController : Controller
    {
        private readonly AppDbContext _context;

        public GenreController(AppDbContext context)
        {
            _context = context;
        }

        // GenreId'ye göre filmleri listele
        public IActionResult MoviesByGenre(int genreId)
        {
            var movies = _context.Movies
                .Where(m => m.MovieGenres.Any(mg => mg.GenreId == genreId))  // GenreId'ye göre filtreleme
                .ToList();

            // Türün adını View'a gönderebiliriz
            var genreName = _context.Genres
                .Where(g => g.GenreId == genreId)
                .Select(g => g.Name)
                .FirstOrDefault();

            ViewData["GenreName"] = genreName;

            return View(movies);  // MoviesByGenre view'ını döndürüyoruz
        }
    }
}
