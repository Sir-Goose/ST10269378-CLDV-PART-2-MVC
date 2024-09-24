using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace st10269378.Services
{
    // queue service for handling azure queue operations
    public class QueueService
    {
        // queue service client for interacting with azure queues
        private readonly QueueServiceClient _queueServiceClient;

        // initializes queue service with configuration
        public QueueService(IConfiguration configuration)
        {
            // creates a new queue service client using the azure storage connection string
            _queueServiceClient = new QueueServiceClient(configuration["AzureStorage:ConnectionString"]);
        }

        // sends a message to the specified azure queue
        public async Task SendMessageAsync(string queueName, string message)
        {
            // gets the queue client for the specified queue
            var queueClient = _queueServiceClient.GetQueueClient(queueName);
            // creates the queue if it does not exist
            await queueClient.CreateIfNotExistsAsync();
            // sends the message to the azure queue
            await queueClient.SendMessageAsync(message);
        }
    }
}