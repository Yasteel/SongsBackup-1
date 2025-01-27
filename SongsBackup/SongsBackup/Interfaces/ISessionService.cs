namespace SongsBackup.Interfaces
{
    using SongsBackup.Models;
    using SongsBackup.Models.SpotifyModels;

    public interface ISessionService
    {
        void SetSessionData(SpotifyTokenResponse token);

        SpotifyTokenResponse GetSessionData();
        
        void SetUserSession(string username, string userImage);
        
        UserSession GetUserSession();
    }
}