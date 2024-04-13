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
        /// Get a Signed URL to acces a particular file by it's name. The link is valid for 60 minutes
        /// </summary>
        Task<FileDownloadRes> GetSignedURLAsync(string fileName);   
    }
}