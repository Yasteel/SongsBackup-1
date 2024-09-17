namespace SongsBackup.Services
{
    using Azure.Storage.Blobs.Models;
    using Azure.Storage.Blobs;
    using Interfaces;

    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private const string BlobContainerName = "songs-backup";

        public BlobService(IConfiguration configuration)
        {
            var blobConnection = configuration.GetConnectionString("Azurite");

            _blobServiceClient = new (blobConnection);
        }
        
        public async Task<string> UploadFilesAsync(IFormFile file)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(BlobContainerName);
            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            var blobClient = blobContainerClient.GetBlobClient(file.FileName);
            await using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            }
            return blobClient.Uri.ToString();
        }
    }
}