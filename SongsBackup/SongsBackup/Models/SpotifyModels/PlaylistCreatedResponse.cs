namespace SongsBackup.Models.SpotifyModels
{
    using Newtonsoft.Json;

    public class PlaylistCreatedResponse
    {

        [JsonProperty("collaborative")] public bool Collaborative { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("external_urls")] public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("followers")] public Followers Followers { get; set; }

        [JsonProperty("href")] public string Href { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("images")] public object[] Images { get; set; }

        [JsonProperty("primary_color")] public object PrimaryColor { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("uri")] public string Uri { get; set; }

        [JsonProperty("owner")] public Owner Owner { get; set; }

        [JsonProperty("public")] public bool Public { get; set; }

        [JsonProperty("snapshot_id")] public string SnapshotId { get; set; }

        [JsonProperty("tracks")] public Tracks Tracks { get; set; }

    }
}
