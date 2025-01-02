using Microsoft.AspNetCore.Mvc;

namespace SongsBackup.Controllers;

public class ManageSongsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}