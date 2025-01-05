namespace SongsBackup.Interfaces
{
    using SongsBackup.Models;

    public interface ISongService
    {
        Task<List<string>> GetSongsAsync();

        List<MetadataModel> ProcessSongs(List<string> songs);

        Task<string> UploadSongs(IFormFile file);

    }
}