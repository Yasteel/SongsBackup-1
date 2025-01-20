using SongsBackup.Models.SpotifyModels.Dto;
using SongsBackup.Models.SpotifyModels.RequestModel;

namespace SongsBackup.Controllers.Api
{
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    
    public class SongsController : ApiBaseController
    {
        private readonly ISongService _songService;
        private readonly ISpotifyService _spotifyService;

        public SongsController(ISongService songService, ISpotifyService spotifyService)
        {
            _songService = songService;
            _spotifyService = spotifyService;
        }
        
        
        [HttpPost("upload")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            if (!files.Any())
            {
                return BadRequest("No files received.");
            }

            var uploadedFileUrls = new List<string>();

            foreach (var file in files)
            {
                if (file.ContentType != "audio/mpeg" && Path.GetExtension(file.FileName).ToLower() != ".mp3")
                {
                    return BadRequest("Invalid file type.");
                }

                var fileUrl = await _songService.UploadSongs(file);
                uploadedFileUrls.Add(fileUrl);
            }

            return Ok(new { Files = uploadedFileUrls });
        }
        
        [HttpPost("upload-songs")]
        public async Task<IActionResult> UploadSongs(List<IFormFile> files)
        {
            if (files.Count == 0)
            {
                return BadRequest("No files received from the request.");
            }

            foreach (var file in files)
            {
                await _songService.UploadSongs(file);
            }

            return Ok(new { Message = "Files uploaded to Azure Blob Storage successfully" });
        }

        [HttpGet("get-songs")]
        public async Task<IActionResult> GetFiles()
        {
            var songs = await _songService.ReadAllMetaDataAsync();
            return Ok(new { Songs = songs });
        }

        [HttpGet("get-playlists")]
        public async Task<IActionResult> GetPlaylists()
        {
            var playlists = await _spotifyService.GetUserPlaylists();

            if (playlists == null)
            {
                return BadRequest();
            }
            
            return Ok(new { Playlists = playlists });
        }
        
        [HttpPost("add-to-playlist")]
        public async Task<IActionResult> AddToPlaylist([FromBody]AddToPlaylistDto model)
        {
            var response = await _spotifyService.AddToPlaylist(model);
            
            if (response == null)
            {
                return BadRequest();
            }
            
            return Ok(new { Message = "Songs added to playlist successfully" });
        }
        
        [HttpPost("create-playlist")]
        public async Task<IActionResult> CreatePlaylist([FromBody]CreatePlaylistRequestModel model)
        {
            var response = await _spotifyService.CreatePlaylist(model);

            if (response == null)
            {
                return BadRequest();
            }
            
            return Ok(new { Message = "Playlist created successfully" });
        }
        
    }
}