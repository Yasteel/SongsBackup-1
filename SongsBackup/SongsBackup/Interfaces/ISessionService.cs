namespace SongsBackup.Interfaces
{
    using SongsBackup.Models.SpotifyModels;

    public interface ISessionService
    {
        void SetSessionData(SpotifyTokenResponse token);

        SpotifyTokenResponse GetSessionData();
    }
}