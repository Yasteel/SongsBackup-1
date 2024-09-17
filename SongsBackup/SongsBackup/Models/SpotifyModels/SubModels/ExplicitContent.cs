namespace SongsBackup.Models.SpotifyModels
{
    using Newtonsoft.Json;

    public class ExplicitContent
    {
        [JsonProperty("filter_enabled")] public bool FilterEnabled { get; set; }

        [JsonProperty("filter_locked")] public bool FilterLocked { get; set; }
    }
}