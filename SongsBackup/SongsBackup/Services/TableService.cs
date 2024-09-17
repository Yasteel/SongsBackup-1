namespace SongsBackup.Services
{
    using Azure.Data.Tables;

    using Interfaces;

    public class TableService : ITableService
    {
        private readonly IConfiguration _configuration;
        private readonly TableServiceClient _tableServiceClient;
        private readonly string _tableName = "FileProcessingStatus";

        public TableService(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("Azurite");
            _tableServiceClient = new (connectionString);
        }
        
        public async Task<List<TableEntity>> GetFilesByStatusAsync(string status)
        {
            var tableClient = _tableServiceClient.GetTableClient(_tableName);
            var entities = new List<TableEntity>();

            // Define the filter condition
            string filter = $"status eq '{status}'";

            // Query the table
            await foreach (var entity in tableClient.QueryAsync<TableEntity>(filter))
            {
                entities.Add(entity);
            }

            return entities;
        }

        public async Task AddOrUpdateAsync(string fileName, string status)
        {
            var tableClient = _tableServiceClient.GetTableClient(_tableName);
            
            // Create Table if it does not exist
            await tableClient.CreateIfNotExistsAsync();

            var entity = new TableEntity("ProcessedFiles", fileName)
            {
                { "status", status }
            };

            await tableClient.AddEntityAsync(entity);
        }
        
        public async Task DeleteFileStatusAsync(string fileName)
        {
            var tableClient = _tableServiceClient.GetTableClient(_tableName);
            await tableClient.DeleteEntityAsync("ProcessedFiles", fileName);
        }
    }
}