namespace SongsBackup.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Interfaces;
    using ViewModel;

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

            return View(new HomeViewModel() { DisplayName = userData.Username, ProfileImage = userData.UserImage });
        }
    }
}