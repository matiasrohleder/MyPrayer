using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Upload file
        /// </summary>
        Task UploadAsync(IFormFile file);
        /// <summary>
        /// Download file by name
        /// </summary>
        Task<byte[]> DownloadAsync(string fileName);   
    }
}