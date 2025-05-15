using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlobStorage.Domain;
using BlobInfo = AzureBlobStorage.Domain.BlobInfo;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AzureBlobStorage.AzureStorage;

namespace AzureBlobStorage.AsureStorage
{
    public class BlobService : IBlobService
    {
        private readonly AzureSettings azureSettings;

        public BlobService(AzureSettings azureSettings)
        {
            this.azureSettings = azureSettings;
        }

        public async Task<BlobInfo> GetBlobAsync(string name)
        {
            var containerClient = azureSettings.ServiceClient.GetBlobContainerClient(azureSettings.ContainerName);
            var blobClient = containerClient.GetBlobClient(name);
            var blobDownloadInfo = await blobClient.DownloadAsync();
            return new BlobInfo(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
        }

        public async Task UploadContentBlobAsync(string content, string fileName)
        {
            var containerClient = azureSettings.ServiceClient.GetBlobContainerClient(azureSettings.ContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var bytes = Encoding.UTF8.GetBytes(content);
            using (var memoryStream = new MemoryStream(bytes))
            {
                await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = fileName.GetContentType() });
            }
        }

        public async Task UploadFileBlobAsync(string filePath, string fileName)
        {
            var containerClient = azureSettings.ServiceClient.GetBlobContainerClient(azureSettings.ContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(filePath, new BlobHttpHeaders { ContentType = filePath.GetContentType() });
        }

        public async Task DeleteFileBlobAsync(string blobName)
        {
            var containerClient = azureSettings.ServiceClient.GetBlobContainerClient(azureSettings.ContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
