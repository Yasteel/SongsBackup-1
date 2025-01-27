namespace SongsBackup.Services
{
    using AutoMapper;
    using Azure.Storage.Blobs.Models;
    using Azure.Storage.Blobs;
    using Interfaces;
    using Models;
    using Models.SpotifyModels.Dto;
    using Models.SpotifyModels.SubModels;

    public class SongService : ISongService
    {
        private readonly ISpotifyService _spotifyService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly BlobServiceClient _blobServiceClient;
        private const string BlobContainerName = "songs-backup";

        public SongService(IConfiguration configuration, ISpotifyService spotifyService, IMapper mapper, ISessionService sessionService)
        {
            var blobConnection = configuration.GetConnectionString("Azurite");

            _blobServiceClient = new (blobConnection);
            _spotifyService = spotifyService;
            _mapper = mapper;
            _sessionService = sessionService;
        }
        
        public async Task<List<SongSearchResultDto>> ReadAllMetaDataAsync()
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(BlobContainerName);
            List<Items> songsObject = new ();

            await foreach (var blobItem in blobContainer.GetBlobsAsync(prefix: $"{GetUsername()}/"))
            {
                var blobClient = blobContainer.GetBlobClient(blobItem.Name);
                var fileMetadata = await ReadMetadataAsync(blobClient);
                var tracks = await _spotifyService.SearchSongsAsync(fileMetadata);

                if (tracks != default)
                {
                    songsObject.Add(tracks.First());
                }
            }
            
            return _mapper.Map<List<SongSearchResultDto>>(songsObject);
        }
        
        private async Task<MetadataModel> ReadMetadataAsync(BlobClient blobClient)
        {
            var memoryStream = new MemoryStream();
            await blobClient.DownloadToAsync(memoryStream);
            memoryStream.Position = 0;
            
            var file = TagLib.File.Create(new StreamFileAbstraction(blobClient.Name, memoryStream, memoryStream));
            
            return new MetadataModel
            {
                Title = file.Tag.Title,
                Album = file.Tag.Album,
                Artist = string.Join(",", file.Tag.Performers.ToArray())
            };
        }
        
        public async Task<string> UploadSongs(IFormFile file)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(BlobContainerName);
            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobName = $"{GetUsername()}/{file.FileName}";
            var blobClient = blobContainerClient.GetBlobClient(blobName);
            await using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            }
            return blobClient.Uri.ToString();
        }

        private string GetUsername()
        {
            var userData = _sessionService.GetUserSession();
            return userData.Username;
        }
    }

    internal class StreamFileAbstraction : TagLib.File.IFileAbstraction
    {
        public string Name { get; set; }
        
        public Stream ReadStream { get; set; }
        
        public Stream WriteStream { get; set; }
        
        
        public StreamFileAbstraction(string name, MemoryStream readStream, MemoryStream writeStream)
        {
            Name = name;
            ReadStream = readStream;
            WriteStream = writeStream;
        }
        
        public void CloseStream(Stream stream)
        {
            stream.Close();
        }
    }
}