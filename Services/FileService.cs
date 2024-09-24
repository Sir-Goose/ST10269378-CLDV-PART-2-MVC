using Azure.Storage.Files.Shares;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace st10269378.Services
{
    // file service for handling azure file share operations
    public class FileService
    {
        // share service client for interacting with azure file shares
        private readonly ShareServiceClient _shareServiceClient;

        // initializes file service with configuration
        public FileService(IConfiguration configuration)
        {
            // creates a new share service client using the azure storage connection string
            _shareServiceClient = new ShareServiceClient(configuration["AzureStorage:ConnectionString"]);
        }

        // uploads a file to azure file share
        public async Task UploadFileAsync(string shareName, string fileName, Stream content)
        {
            // gets the share client for the specified share
            var shareClient = _shareServiceClient.GetShareClient(shareName);
            // creates the share if it does not exist
            await shareClient.CreateIfNotExistsAsync();
            // gets the root directory client for the share
            var directoryClient = shareClient.GetRootDirectoryClient();
            // gets the file client for the specified file
            var fileClient = directoryClient.GetFileClient(fileName);
            // creates the file and sets its length
            await fileClient.CreateAsync(content.Length);
            // uploads the file content to azure file share
            await fileClient.UploadAsync(content);
        }
    }
}