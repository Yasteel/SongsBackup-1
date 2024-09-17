namespace SongsBackup.Controllers.Api
{
    using Azure.Data.Tables;

    using Microsoft.AspNetCore.Mvc;
    
    using Interfaces;

    public class TestController(ITableService tableService) : ApiBaseController
    {
        [HttpGet("get")]
        public async Task<List<TableEntity>> TestGet()
        {
            var entities = await tableService.GetFilesByStatusAsync("success");
            return entities;
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateTable(string fileName, string status)
        {
            try
            {
                await tableService.AddOrUpdateAsync(fileName, status);
                return this.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    }
}