namespace SongsBackup.Interfaces
{
    using Azure.Data.Tables;

    public interface ITableService
    {
        Task<List<TableEntity>> GetFilesByStatusAsync(string status);

        Task AddOrUpdateAsync(string fileName, string status);

        Task DeleteFileStatusAsync(string fileName);
    }
}