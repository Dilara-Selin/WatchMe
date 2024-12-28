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
        public async Task<IActionResult> RandomMovies()
{
    var movies = await _context.Movies
        .OrderBy(x => Guid.NewGuid()) // Rastgele sıralama
        .Take(6) // Örnek olarak 6 film al
        .ToListAsync();
    return PartialView("_NetflixCard", movies);
}
public async Task<IActionResult> PopularMovies()
{
    var movies = await _context.Movies
        .OrderByDescending(m => m.Popularity) // Popülerliğe göre sıralama
        .Take(6) // İlk 6 filmi al
        .ToListAsync();
    return PartialView("_NetflixCard", movies);
}
public async Task<IActionResult> NewReleases()
{
    var movies = await _context.Movies
        .OrderByDescending(m => m.ReleaseDate) // Çıkış tarihine göre sıralama
        .Take(6) // İlk 6 filmi al
        .ToListAsync();
    return PartialView("_NetflixCard", movies);
}

    }
}
