namespace SongsBackup.Models.SpotifyModels
{
    using Newtonsoft.Json;
    using SubModels;

    public class SearchResponse
    {
        [JsonProperty("tracks")]
        public Tracks tracks { get; set; }
    }
}