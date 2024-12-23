using Microsoft.AspNetCore.Mvc;

namespace watch_me.Controllers;

public class ProfileController : Controller
{
    public IActionResult Profile()
    {
            ViewData["ShowHeaderFooter"] = true;
        return View();
    }
}