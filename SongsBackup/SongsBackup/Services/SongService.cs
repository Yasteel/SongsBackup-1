
namespace SongsBackup.Services
{
    using Azure.Storage.Blobs.Models;
    using Azure.Storage.Blobs;

    using Interfaces;

    using SongsBackup.Models;

    public class SongService : ISongService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private const string BlobContainerName = "songs-backup";

        public SongService(IConfiguration configuration)
        {
            var blobConnection = configuration.GetConnectionString("Azurite");

            _blobServiceClient = new (blobConnection);
        }
        
        public async Task<List<string>> GetSongsAsync()
        {
            var files = new List<string>();

            try
            {
                // Get reference to the container
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(BlobContainerName);

                // Ensure the container exists
                if (await containerClient.ExistsAsync())
                {
                    // List blobs in the container
                    await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
                    {
                        files.Add(blobItem.Name);
                    }
                }
                else
                {
                    Console.WriteLine($"Container '{BlobContainerName}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving files: {ex.Message}");
            }

            return files;
        }
        
        public List<MetadataModel> ProcessSongs(List<string> songs)
        {
            List<MetadataModel> songsObject = new ();
            
            foreach (var song in songs)
            {
                var tfile = TagLib.File.Create(song);
                songsObject.Add(new ()
                {
                    Title = tfile.Tag.Title,
                    Album = tfile.Tag.Album,
                    Artist = tfile.Tag.Performers.ToArray()
                    
                });
            }

            return songsObject;
        }
        
        public async Task<string> UploadSongs(IFormFile file)
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