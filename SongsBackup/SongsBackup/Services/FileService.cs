namespace SongsBackup.Services
{
    using SongsBackup.Models;
    using SongsBackup.Interfaces;
    
    public class FileService : IFileService
    {
        public List<string> GetSongs(string dir)
        {
            var files = Directory.GetFiles(dir).AsQueryable()
                .Where(_ => _.EndsWith(".mp3") || _.EndsWith(".m4a"))
                .ToList();
            return files;
        }

        public List<MetadataModel> ProcessSongs(List<string> songs)
        {
            List<MetadataModel> songsObject = new ();
            
            foreach (var song in songs)
            {
                var tfile = TagLib.File.Create(song);
                songsObject.Add(new ()
                {
                    Title = tfile.Tag.Title,
                    Album = tfile.Tag.Album,
                    Artist = tfile.Tag.Artists
                    
                });
            }

            return songsObject;
        }
    }
}

