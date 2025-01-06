using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using WatchMe.Protos;
using Grpc.Net.Client;

namespace WatchMe.Controllers
{
    public class LikedController : Controller
    {

        private readonly MovieService _movieService;
        private readonly AppDbContext _context;
        private readonly ILogger<MovieController> _logger;

        public LikedController(MovieService movieService, AppDbContext context, ILogger<MovieController> logger)
        {
            _movieService = movieService;
            _context = context;
            _logger = logger;
      
        }



public async Task<IActionResult> LikedMovies()
{
    // gRPC istemcisi oluştur (MovieLikeServiceClient)
    var channel = GrpcChannel.ForAddress("https://localhost:5001");
    var client = new MovieLikeService.MovieLikeServiceClient(channel);

    // Kullanıcı kimliğini al
    string userIdString = "1"; // Örnek kullanıcı kimliği
    int userId = int.Parse(userIdString);

    // gRPC servisine istek gönder
    var request = new LikedMoviesRequest { UserId = userId };
    var response = await client.GetLikedMoviesAsync(request);

    // gRPC'den dönen filmleri Partial View'a gönder
    var likedMovies = response.Movies
        .Select(m => new WatchMe.Models.Movie
        {
            MovieId = m.Id,
            Title = m.Title,
            PosterPath = m.PosterPath,
            Popularity = m.Popularity
        }).ToList();

    return PartialView("_LikedCard", likedMovies);
}

    }
    
}