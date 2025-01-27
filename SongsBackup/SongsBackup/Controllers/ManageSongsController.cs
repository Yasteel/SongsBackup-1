using Microsoft.AspNetCore.Mvc;
using SongsBackup.Interfaces;
using SongsBackup.ViewModel;

namespace SongsBackup.Controllers;

public class ManageSongsController : Controller
{
    private readonly ISessionService _sessionService;

    public ManageSongsController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public IActionResult Index()
    {
        var userData = _sessionService.GetUserSession();
        
        return View(new HomeViewModel(){ DisplayName = userData.Username, ProfileImage = userData.UserImage });
    }
}