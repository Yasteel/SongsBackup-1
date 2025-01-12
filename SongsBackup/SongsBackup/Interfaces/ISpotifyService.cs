namespace SongsBackup.Interfaces
{
    using Models;
    using Models.SpotifyModels;
    using Models.SpotifyModels.Dto;
    using Models.SpotifyModels.RequestModel;
    using Models.SpotifyModels.SubModels;

    public interface ISpotifyService
    {
        Task<ProfileResponse?> GetProfile();

        bool IsTokenExpired();
        
        Task<List<Items>?> SearchSongsAsync(MetadataModel songObject);

        Task<PlaylistCreatedResponse?> CreatePlaylist(CreatePlaylistRequestModel model);

        Task<List<UserPlaylistDto>?> GetUserPlaylists();

        Task<object?> AddToPlaylist(AddToPlaylistDto model);
    }
}

