namespace SongsBackup.Models.SpotifyModels
{
    using Newtonsoft.Json;
    
    public class ExternalIds
    {
        [JsonProperty("isrc")]
        public string Isrc { get; set; }
    }
}