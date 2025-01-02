using Microsoft.AspNetCore.Mvc;
using WatchMe.Models;
using WatchMe.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WatchMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Privacy()
        {
            ViewData["ShowHeaderFooter"] = true;
            return View();
        }

        [HttpGet("LoginSuccess")]
        public IActionResult LoginSuccess()
        {
            return View(); // LoginSuccess.cshtml
        }

        public IActionResult Index()
        {
            var allMovies = _context.Movies.ToList(); // Tüm filmleri getir
            return View(allMovies); // View'a gönder
        }

        public IActionResult WelcomePage()
        {
            ViewData["ShowHeaderFooter"] = false;
            return View();
        }

        public IActionResult Login()
        {
            ViewData["ShowHeaderFooter"] = false;
            return View();
        }

        public IActionResult Register()
        {
            ViewData["ShowHeaderFooter"] = false;
            return View();
        }

        public IActionResult ForgotPassword()
        {
            ViewData["ShowHeaderFooter"] = false;
            return View();
        }

        public IActionResult Profile()
        {
            ViewData["ShowHeaderFooter"] = true;
            return View();
        }

        public IActionResult Netflix()
        {
            var allMovies = _context.Movies.ToList();
            return View(allMovies);
        }

    }
}
