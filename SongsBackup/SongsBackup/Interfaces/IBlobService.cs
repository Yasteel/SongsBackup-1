namespace SongsBackup.Interfaces;

public interface IBlobService
{
    Task<string> UploadFilesAsync(IFormFile file);
}