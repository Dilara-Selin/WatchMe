using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using WatchMe.Data;
using WatchMe.Protos;

public class MovieLikeServiceImpl : MovieLikeService.MovieLikeServiceBase
{
    private readonly AppDbContext _context;

    public MovieLikeServiceImpl(AppDbContext context)
    {
        _context = context;
    }

    public override async Task<LikedMoviesResponse> GetLikedMovies(LikedMoviesRequest request, ServerCallContext context)
    {
        var likedMovies = await _context.MovieLikes
            .Include(ml => ml.Movie)
            .Where(ml => ml.UserId == request.UserId)
            .Select(ml => new Movie
            {
                Id = ml.Movie.MovieId,
                Title = ml.Movie.Title,
                PosterPath = ml.Movie.PosterPath,
                Popularity = ml.Movie.Popularity ?? 0.0
            })
            .ToListAsync();

        var response = new LikedMoviesResponse();
        response.Movies.AddRange(likedMovies);

        return response;
    }
}
