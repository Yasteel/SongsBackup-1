namespace SongsBackup.Models.SpotifyModels
{
    using Newtonsoft.Json;

    public class Items
    {
        [JsonProperty("album")]
        public Album Album { get; set; }
        
        [JsonProperty("artists")]
        public Artists[] Artists { get; set; }
        
        [JsonProperty("external_ids")]
        public ExternalIds ExternalIds { get; set; }
        
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }
        
        [JsonProperty("href")]
        public string Href { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }
        
        [JsonProperty("track_number")]
        public int TrackNumber { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}