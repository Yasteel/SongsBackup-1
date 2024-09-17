namespace SongsBackup.Interfaces
{
    using SongsBackup.Models;
    using SongsBackup.Models.SpotifyModels;
    using SongsBackup.Models.SpotifyModels.RequestModel;

    public interface ISpotifyService
    {
        Task<ProfileResponse?> GetProfile();

        bool IsTokenExpired();
        
        Task<string?> SearchSongsAsync(MetadataModel songObject);

        Task<PlaylistCreatedResponse?> CreatePlaylist(CreatePlaylistRequestModel model);

        Task<UserPlaylistResponse?> GetUserPlaylists();

        Task<object> AddToPlaylist(string playlistId, AddToPlaylistRequestModel songs);
    }
}

