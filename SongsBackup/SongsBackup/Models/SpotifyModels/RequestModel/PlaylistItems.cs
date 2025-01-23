namespace SongsBackup.Models.SpotifyModels.RequestModel
{
    public class PlaylistItems
    {
        public string href { get; set; }
        public List<TrackItems> items { get; set; }
    }

    public class TrackItems
    {
        public Track track { get; set; }
    }

    public class Track
    {
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string uri { get; set; }
    }

}