using Azure.Storage.Blobs;

namespace SongsBackup.Controllers.Api
{
    using Interfaces;

    using Microsoft.AspNetCore.Mvc;
    
    public class SongsController : ApiBaseController
    {
        private readonly ISongService _songService;

        public SongsController(ISongService songService)
        {
            this._songService = songService;
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

                var fileUrl = await this._songService.UploadSongs(file);
                uploadedFileUrls.Add(fileUrl);
            }

            return Ok(new { Files = uploadedFileUrls });
        }
        
        [HttpPost("upload-songs")]
        public async Task<IActionResult> UploadSongs(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files received from the request.");
            }

            foreach (var file in files)
            {
                await this._songService.UploadSongs(file);
            }

            return Ok(new { Message = "Files uploaded to Azure Blob Storage successfully" });
        }

        [HttpGet("get-songs")]
        public async Task<IActionResult> GetFiles()
        {
            var songs = await this._songService.ReadAllMetaDataAsync();
            
            return this.Ok(new { Songs = songs });
        }
        
    }
}