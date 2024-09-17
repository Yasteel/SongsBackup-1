namespace SongsBackup.Controllers
{
    using Interfaces;
    
    using Microsoft.AspNetCore.Mvc;

    using Models.SpotifyModels;

    using ViewModel;

    public class LandingController(ISpotifyService spotifyService) : Controller
    {
        public async Task<IActionResult> Index(HomeViewModel? viewModel)
        {
            var profile = await spotifyService.GetProfile();
            return this.View(this.BuildViewModel(profile));
        }
        
        public IActionResult Connect()
        {
            return this.RedirectToAction("Login", "Auth");
        }
        
        private HomeViewModel BuildViewModel(ProfileResponse? profile)
        {
            string profileImage;

            if (profile is { Images.Length: 1 })
            {
                profileImage = profile.Images[0].Url;
            }
            else
            {
                var largest = profile?.Images.AsQueryable()
                    .OrderByDescending(_ => _.Width)
                    .ToArray();

                profileImage = largest?[0].Url!;
            }

            return new()
            {
                DisplayName = profile!.DisplayName,
                ProfileImage = profileImage 
            };
        }
    }
}