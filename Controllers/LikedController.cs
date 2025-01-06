using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using WatchMe.Protos;
using Grpc.Net.Client;
using System.Linq;

namespace WatchMe.Controllers
{
    public class LikedController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LikedController> _logger;

        public LikedController(AppDbContext context, ILogger<LikedController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Profil sayfasına gittiğimizde, kullanıcının beğenilen filmlerini alacağız
       public async Task<IActionResult> Profile()
{
    // gRPC istemcisi oluştur
    var channel = GrpcChannel.ForAddress("https://localhost:5001");
    var client = new MovieLikeService.MovieLikeServiceClient(channel);

    // Sabit kullanıcı kimliği (bu kimlik dinamik olmalı)
    int userId = 1;  // Bunu kullanıcı kimliğine göre dinamik hale getirin.

    // gRPC servisine istek gönder
    var request = new LikedMoviesRequest { UserId = userId };
    var response = await client.GetLikedMoviesAsync(request);

    // gRPC'den dönen filmleri model olarak gönder
    var likedMovies = response.Movies
        .Select(m => new WatchMe.Models.Movie
        {
            MovieId = m.Id,
            Title = m.Title,
            PosterPath = m.PosterPath,
            Popularity = m.Popularity
        }).ToList();

    // Verinin doğru şekilde geldiğini kontrol et
    if (likedMovies == null || !likedMovies.Any())
    {
        _logger.LogWarning("No liked movies found for user with ID: " + userId);
    }

    return View(likedMovies); // PartialView yerine doğrudan View'ye gönderiyoruz
}}}