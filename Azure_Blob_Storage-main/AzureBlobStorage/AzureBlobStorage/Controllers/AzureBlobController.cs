using AzureBlobStorage.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBlobStorage.Controllers
{
    [Route("blobs")]
    public class AzureBlobController : Controller
    {
        private IBlobService _blobService;

        public AzureBlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet("{blobName}")]
        public async Task<ActionResult> GetBlob(string blobName)
        {
            var data = await _blobService.GetBlobAsync(blobName);
            return File(data.Content, data.ContentType);
        }

        [HttpPost("uploadfile")]
        public async Task<ActionResult> UploadFile([FromBody] UploadFile uploadFile)
        {
            await _blobService.UploadFileBlobAsync(uploadFile.FilePath, uploadFile.FileName);
            return Ok();
        }

        [HttpPost("uploadcontent")]
        public async Task<ActionResult> UploadContent([FromBody] UploadContent uploadContent)
        {
            await _blobService.UploadContentBlobAsync(uploadContent.Content, uploadContent.FileName);
            return Ok();
        }

        [HttpDelete("deleteblob")]
        public async Task<ActionResult> DeleteBlob(string blobName)
        {
            await _blobService.DeleteFileBlobAsync(blobName);
            return Ok();
        }

    }
}
