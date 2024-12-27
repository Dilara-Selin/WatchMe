using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchMe.Data;
using WatchMe.Models;

namespace WatchMe.Controllers
{
    public class GenreController : Controller
{
    private readonly AppDbContext _context;

    public GenreController(AppDbContext context)
    {
        _context = context;
    }

    // Genre sayfasını render et
    public async Task<IActionResult> MoviesByGenre(int genreId)
    {
        var genre = await _context.Genres
            .Include(g => g.MovieGenres!)
            .ThenInclude(mg => mg.Movie)
            .FirstOrDefaultAsync(g => g.GenreId == genreId);

        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);  // Genre'ya ait filmleri gösteren view
    }

    // Kullanıcının bir tür seçmesini sağlayacak sayfa
    public async Task<IActionResult> SelectGenre()
    {
        var genres = await _context.Genres.ToListAsync();
        return View(genres);  // Tüm genre'leri gösteren view
    }
}

}
