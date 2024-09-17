namespace SongsBackup.Controllers.Api
{
    using Interfaces;

    using Microsoft.AspNetCore.Mvc;
    
    public class FilesController : ApiBaseController
    {
        private readonly IBlobService _blobService;

        public FilesController(IBlobService blobService)
        {
            this._blobService = blobService;
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

                var fileUrl = await this._blobService.UploadFilesAsync(file);
                uploadedFileUrls.Add(fileUrl);
            }

            // TODO: Trigger processing of the uploaded files here

            return Ok(new { Files = uploadedFileUrls });
        }
        
    }
}