using Microsoft.AspNetCore.Mvc;

namespace WatchMe.Controllers;

public class ProfileController : Controller
{
    public IActionResult Profile()
    {
            ViewData["ShowHeaderFooter"] = true;
        return View();
    }
    
}