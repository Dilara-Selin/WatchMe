using Microsoft.AspNetCore.Mvc;
using WatchMe.Models;
using System.Diagnostics;
using WatchMe.Data;
using Microsoft.EntityFrameworkCore;

namespace WatchMe.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Privacy()
        {
                ViewData["ShowHeaderFooter"] = true;
            return View();
        }
         [HttpGet("LoginSuccess")]
    public IActionResult LoginSuccess()
    {
        return View();  // Returns the LoginSuccess.cshtml view
    }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
                ViewData["ShowHeaderFooter"] = true;
            return View();
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
            ViewData["ShowHeaderFooter"] = true;
        return View();
    }
    }
}