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
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}