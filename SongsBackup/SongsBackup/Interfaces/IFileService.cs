using SongsBackup.Models;

namespace SongsBackup.Interfaces
{
    public interface IFileService
    {
        List<string> GetSongs(string dir);

        List<MetadataModel> ProcessSongs(List<string> songs);
    }
}

