namespace SongsBackup.Models.SpotifyModels.RequestModel
{
    using Newtonsoft.Json;

    public class AddToPlaylistRequestModel
    {
        [JsonProperty("uris")] public string[] Uris { get; set; }

        [JsonProperty("postition")] public int? Position { get; set; }
    }
}