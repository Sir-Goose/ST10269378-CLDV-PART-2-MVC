using Azure;
using Azure.Data.Tables;
using st10269378.Models;
using System.Threading.Tasks;

namespace st10269378.Services
{
    // table service for handling azure table operations
    public class TableService
    {
        // table client for interacting with azure table
        private readonly TableClient _tableClient;

        // initializes table service with configuration
        public TableService(IConfiguration configuration)
        {
            // creates a new table service client using the azure storage connection string
            var serviceClient = new TableServiceClient(configuration["AzureStorage:ConnectionString"]);
            // gets the table client for the specified table
            _tableClient = serviceClient.GetTableClient("CustomerProfiles");
            // creates the table if it does not exist
            _tableClient.CreateIfNotExists();
        }

        // adds a new entity to the specified azure table
        public async Task AddEntityAsync(CustomerProfile profile)
        {
            // adds the customer profile to the azure table
            await _tableClient.AddEntityAsync(profile);
        }
    }
}