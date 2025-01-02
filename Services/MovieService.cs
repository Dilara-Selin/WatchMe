using WatchMe.Models;
using Microsoft.EntityFrameworkCore;
using WatchMe.Data;  // AppDbContext'in bulunduğu namespace

public class MovieService
{
    private readonly AppDbContext _context;

    public MovieService(AppDbContext context)
    {
        _context = context;
    }

    // Film beğenme işlemi
    public async Task<bool> LikeMovieAsync(int userId, int movieId)
    {
        var movieLike = await _context.MovieLikes
            .FirstOrDefaultAsync(m => m.UserId == userId && m.MovieId == movieId);

        if (movieLike != null) return false; // Zaten beğenilmiş

        var newLike = new MovieLike
        {
            MovieId = movieId,
            UserId = userId
        };

        _context.MovieLikes.Add(newLike);
        await _context.SaveChangesAsync();

        return true;
    }

    // Film beğenmeme işlemi
    public async Task<bool> DislikeMovieAsync(int userId, int movieId)
    {
        var movieDislike = await _context.MovieDislikes
            .FirstOrDefaultAsync(m => m.UserId == userId && m.MovieId == movieId);

        if (movieDislike != null) return false; // Zaten beğenilmemiş

        var newDislike = new MovieDislike
        {
            MovieId = movieId,
            UserId = userId
        };

        _context.MovieDislikes.Add(newDislike);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddToWatchListAsync(int userId, int movieId)
{
    // Eğer zaten izleme listesinde varsa, false döndür
    var existingEntry = await _context.MovieWatchLists
        .FirstOrDefaultAsync(w => w.UserId == userId && w.MovieId == movieId);

    if (existingEntry != null) return false;

    // Yeni bir izleme listesi girdisi oluştur
    var watchListEntry = new MovieWatchList
    {
        UserId = userId,
        MovieId = movieId
    };

    _context.MovieWatchLists.Add(watchListEntry);
    await _context.SaveChangesAsync();

    return true;
}
 public async Task<List<MovieComment>> GetCommentsAsync(int movieId)
{
    // MovieComments tablosundaki yorumları al
    var comments = await _context.MovieComments
    .FromSqlRaw("SELECT * FROM public.\"MovieComments\" WHERE \"MovieId\" = {0}", movieId)
    .ToListAsync();

    
    return comments;
}


    // Yorum ekle (sp_insert_movie_comment)
    public async Task AddCommentAsync(int movieId, int userId, string comment)
    {
        // Yorum ekleme prosedürünü çağırıyoruz
        await _context.Database.ExecuteSqlRawAsync("CALL public.sp_insert_movie_comment({0}, {1}, {2})", movieId, userId, comment);
    }

    // Yorum güncelle (sp_update_movie_comment)
    public async Task UpdateCommentAsync(int commentId, string newComment)
    {
        // Yorum güncelleme prosedürünü çağırıyoruz
        await _context.Database.ExecuteSqlRawAsync("CALL public.sp_update_movie_comment({0}, {1})", commentId, newComment);
    }

    // Yorum sil (sp_delete_movie_comment)
    public async Task DeleteCommentAsync(int commentId)
    {
        // Yorum silme prosedürünü çağırıyoruz
        await _context.Database.ExecuteSqlRawAsync("CALL public.sp_delete_movie_comment({0})", commentId);
    }

    // Film bilgilerini ID'ye göre almak
public async Task<Movie?> GetMovieByIdAsync(int id)
{
    return await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == id);
}

}
