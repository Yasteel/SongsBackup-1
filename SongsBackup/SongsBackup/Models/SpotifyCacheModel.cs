namespace SongsBackup.Models.SpotifyModels
{
    public class SpotifyCacheModel
    {
        public string? AccessToken { get; set; }
        
        public DateTime ExpiryTime { get; set; }
    }
}

