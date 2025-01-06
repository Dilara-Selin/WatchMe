using WatchMe.Models;
using Microsoft.EntityFrameworkCore;
using WatchMe.Data;

public class TVShowService
{
    private readonly AppDbContext _context;

    public TVShowService(AppDbContext context)
    {
        _context = context;
    }

    // Dizi beğenme işlemi
    public async Task<bool> LikeTVShowAsync(int userId, int tvShowId)
    {
        var tvShowLike = await _context.TVShowLikes
            .FirstOrDefaultAsync(tsl => tsl.UserId == userId && tsl.TVShowId == tvShowId);

        if (tvShowLike != null) return false; // Zaten beğenilmiş

        var newLike = new TVShowLike
        {
            TVShowId = tvShowId,
            UserId = userId
        };

        _context.TVShowLikes.Add(newLike);
        await _context.SaveChangesAsync();

        return true;
    }

    // Dizi beğenmeme işlemi
    public async Task<bool> DislikeTVShowAsync(int userId, int tvShowId)
    {
        var tvShowDislike = await _context.TVShowDislikes
            .FirstOrDefaultAsync(tsd => tsd.UserId == userId && tsd.TVShowId == tvShowId);

        if (tvShowDislike != null) return false; // Zaten beğenilmemiş

        var newDislike = new TVShowDislike
        {
            TVShowId = tvShowId,
            UserId = userId
        };

        _context.TVShowDislikes.Add(newDislike);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddToWatchListAsync(int userId, int tvShowId)
    {
        // Eğer zaten izleme listesinde varsa, false döndür
        var existingEntry = await _context.TVShowWatchLists
            .FirstOrDefaultAsync(w => w.UserId == userId && w.TVShowId == tvShowId);

        if (existingEntry != null) return false;

        // Yeni bir izleme listesi girdisi oluştur
        var watchListEntry = new TVShowWatchList
        {
            UserId = userId,
            TVShowId = tvShowId
        };

        _context.TVShowWatchLists.Add(watchListEntry);
        await _context.SaveChangesAsync();

        return true;
    }

    // Yorum ekle (sp_insert_tvshow_comment)
    public async Task AddCommentAsync(int tvShowId, int userId, string comment)
    {
        // Yorum ekleme prosedürünü çağırıyoruz
        await _context.Database.ExecuteSqlRawAsync("CALL public.sp_insert_tvshow_comment({0}, {1}, {2})", tvShowId, userId, comment);
    }

    // Yorum güncelle (sp_update_tvshow_comment)
    public async Task UpdateCommentAsync(int commentId, string newComment)
    {
        // Yorum güncelleme prosedürünü çağırıyoruz
        await _context.Database.ExecuteSqlRawAsync("CALL public.sp_update_tvshow_comment({0}, {1})", commentId, newComment);
    }

    // Yorum sil (sp_delete_tvshow_comment)
    public async Task DeleteCommentAsync(int commentId)
    {
        // Yorum silme prosedürünü çağırıyoruz
        await _context.Database.ExecuteSqlRawAsync("CALL public.sp_delete_tvshow_comment({0})", commentId);
    }

    // Dizi bilgilerini ID'ye göre almak
    public async Task<TVShow?> GetTVShowByIdAsync(int id)
    {
        return await _context.TVShows.FirstOrDefaultAsync(ts => ts.TVShowId == id);
    }
}
