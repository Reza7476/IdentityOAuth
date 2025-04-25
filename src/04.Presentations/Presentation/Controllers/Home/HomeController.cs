using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Home;

public class HomeController : Controller
{
    public IActionResult Index( string? message )
    {
        return View();
    }
}
