using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services.FileService;
/// <summary>
/// Service to handle file transactions
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Upload file
    /// </summary>
    Task<FileUploadRes> UploadAsync(IFormFile file);
    
    /// <summary>
    /// Get a Public URL to acces a particular file by it's name. File can be transformed to suit the use case.
    /// </summary>
    FileDownloadRes GetPublicURL(string fileName, FileDownloadReqOptions? options = null);
}