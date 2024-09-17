namespace SongsBackup.Services
{
    using System.Globalization;
    
    using Models.SpotifyModels;
    using Interfaces;
    
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContext;

        public SessionService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        
        public void SetSessionData(SpotifyTokenResponse token)
        {
            _httpContext.HttpContext!.Session.SetString(nameof(SpotifyTokenResponse.AccessToken), token.AccessToken);
            _httpContext.HttpContext!.Session.SetString(nameof(SpotifyTokenResponse.RefreshToken), token.RefreshToken);
            _httpContext.HttpContext!.Session.SetString(nameof(SpotifyTokenResponse.ExpiresAt), DateTime.Now.AddSeconds(token.ExpiresIn).ToString(CultureInfo.InvariantCulture));
        }

        public SpotifyTokenResponse GetSessionData()
        {
            return new()
            {
                AccessToken = _httpContext.HttpContext?.Session.GetString(nameof(SpotifyTokenResponse.AccessToken))!,
                RefreshToken = _httpContext.HttpContext?.Session.GetString(nameof(SpotifyTokenResponse.RefreshToken))!,
                ExpiresAt = _httpContext.HttpContext?.Session.GetString(nameof(SpotifyTokenResponse.ExpiresAt))!
            };
        }
    }
}