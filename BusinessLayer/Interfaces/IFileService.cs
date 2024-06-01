using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Upload file
        /// </summary>
        Task<FileUploadRes> UploadAsync(IFormFile file);
        
        /// <summary>
        /// Get a Public URL to acces a particular file by it's name.
        /// </summary>
        FileDownloadRes GetPublicURL(string fileName, FileDownloadReqOptions? options = null);
    }
}