namespace SongsBackup.Interfaces
{
    using Models.SpotifyModels.Dto;

    public interface ISongService
    {
        Task<List<SpotifyTrackDto>> ReadAllMetaDataAsync();

        Task<string> UploadSongs(IFormFile file);

    }
}