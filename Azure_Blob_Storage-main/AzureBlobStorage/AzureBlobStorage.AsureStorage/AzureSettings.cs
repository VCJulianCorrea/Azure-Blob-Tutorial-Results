using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureBlobStorage.AzureStorage
{
    public class AzureSettings
    {
        public AzureSettings(BlobServiceClient serviceClient, string containerName)
        {
            ServiceClient = serviceClient;
            ContainerName = containerName;
        }

        public BlobServiceClient ServiceClient { get; set; }

        public string ContainerName { get; set; }
    }
}
