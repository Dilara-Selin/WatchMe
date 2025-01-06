using Microsoft.AspNetCore.Mvc;
using WatchMe.Services;  // gRPC servisinizi kullandığınız namespace
using Microsoft.Extensions.Logging; // Logger kullanmak için
using System.Threading.Tasks;
using WatchMe.Models; // Veritabanı modelleriniz veya view modelleri
using WatchMe.Protos;
using Grpc.Net.Client;

namespace WatchMe.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Profile()
        {
            // gRPC istemcisi oluştur
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new MovieLikeService.MovieLikeServiceClient(channel);

            // Kullanıcı kimliği her zaman 1
            int userId = 1;  // Sabit olarak 1 kullanıcı ID'si

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

            // Profil sayfasını ve likedMovies verisini birlikte döndür
            return View(likedMovies); // View'ı döndürerek tüm verileri gönderiyoruz
        }
    }
}