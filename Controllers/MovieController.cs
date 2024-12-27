using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchMe.Data;
using WatchMe.Models;

namespace WatchMe.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context)
        {
            _context = context;
        }
        

        // Movie Detail
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres!)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieComments!)
                .ThenInclude(mc => mc.User)
                .FirstOrDefaultAsync(m => m.MovieId == id);

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
