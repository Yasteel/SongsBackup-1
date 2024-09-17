namespace SongsBackup.Models.SpotifyModels
{
    using Newtonsoft.Json;

    public class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }
}

