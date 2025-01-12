namespace SongsBackup.Interfaces
{
    using Models.SpotifyModels.Dto;

    public interface ISongService
    {
        Task<List<SongSearchResultDto>> ReadAllMetaDataAsync();

        Task<string> UploadSongs(IFormFile file);

    }
}