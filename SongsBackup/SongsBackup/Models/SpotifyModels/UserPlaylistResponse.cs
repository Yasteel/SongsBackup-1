using SongsBackup.Models.SpotifyModels.SubModels;

namespace SongsBackup.Models.SpotifyModels
{
    using Newtonsoft.Json;

    public class UserPlaylistResponse
    {
        [JsonProperty("href")] public string Href { get; set; }

        [JsonProperty("items")] public Items[] Items { get; set; }
    }
}




