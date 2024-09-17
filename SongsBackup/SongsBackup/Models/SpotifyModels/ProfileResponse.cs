namespace SongsBackup.Models.SpotifyModels
{
    using Newtonsoft.Json;

    public class ProfileResponse
    {
        [JsonProperty("display_name")] public string DisplayName { get; set; }

        [JsonProperty("external_urls")] public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("href")] public string Href { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("images")] public Images[] Images { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("uri")] public string Uri { get; set; }

        [JsonProperty("followers")] public Followers Followers { get; set; }

        [JsonProperty("country")] public string Country { get; set; }

        [JsonProperty("product")] public string Product { get; set; }

        [JsonProperty("explicit_content")] public ExplicitContent ExplicitContent { get; set; }

        [JsonProperty("email")] public string Email { get; set; }
    }
}