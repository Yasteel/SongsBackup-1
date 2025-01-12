namespace SongsBackup.Models.SpotifyModels.Dto
{
    using Newtonsoft.Json;

    public class AddToPlaylistDto
    {
        public string[]? Uris { get; set; }

        public string? PlaylistId { get; set; }
    }
}