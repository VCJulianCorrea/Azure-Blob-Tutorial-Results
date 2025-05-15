using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlobStorage.Domain
{
    public interface IBlobService
    {
        Task<BlobInfo> GetBlobAsync(string name);

        Task UploadFileBlobAsync(string filePath, string fileName);

        Task UploadContentBlobAsync(string content, string fileName);

        Task DeleteFileBlobAsync(string blobName);

    }
}
