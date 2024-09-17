namespace SongsBackup.Models.SpotifyModels.RequestModel
{
    using Newtonsoft.Json;

    public class CreatePlaylistRequestModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

    }
}

