using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace st10269378.Services
{
    // blob service for handling azure blob storage operations
    public class BlobService
    {
        // blob service client for interacting with azure blob storage
        private readonly BlobServiceClient _blobServiceClient;

        // initializes blob service with configuration
        public BlobService(IConfiguration configuration)
        {
            // creates a new blob service client using the azure storage connection string
            _blobServiceClient = new BlobServiceClient(configuration["AzureStorage:ConnectionString"]);
        }

        // uploads a blob to azure blob storage
        public async Task UploadBlobAsync(string containerName, string blobName, Stream content)
        {
            // gets the blob container client for the specified container
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            // creates the container if it does not exist
            await containerClient.CreateIfNotExistsAsync();
            // gets the blob client for the specified blob
            var blobClient = containerClient.GetBlobClient(blobName);
            // uploads the blob to azure blob storage
            await blobClient.UploadAsync(content, true);
        }
    }
}