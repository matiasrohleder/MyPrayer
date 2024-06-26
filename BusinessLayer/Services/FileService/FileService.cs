using System.Web;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services.FileService;

/// <inheritdoc />
public class FileService : IFileService
{
    private readonly IFileServiceHelper fileServiceHelper;

    public FileService(IFileServiceHelper fileServiceHelper)
    {
        this.fileServiceHelper = fileServiceHelper;
    }

    /// <inheritdoc />
    public async Task<FileUploadRes> UploadAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new Exception("No file uploaded.");

        var storage = await fileServiceHelper.GetStorage(file.ContentType);

        // Convert IFormFile to byte array
        byte[] fileBytes;
        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms);
            fileBytes = ms.ToArray();
        }

        // Create a unique file name to prevent overwriting existing files (optional)
        var now = DateTime.Now.ToString("yyyyMMddHHmmss");
        string fileName = $"{now}-{file.FileName}";

        // Upload the file
        await storage.Upload(fileBytes, fileName);

        return new FileUploadRes { FileUrl = fileName };
    }

    /// <inheritdoc />
    public FileDownloadRes GetPublicURL(string fileName, FileDownloadReqOptions? options = null)
    {
        if(string.IsNullOrEmpty(fileName))
            return new FileDownloadRes();
        var uriBuilder = this.fileServiceHelper.GetUriForfile(fileName);

        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        if(options is not null){
            var queryParams = options!.ToNameValueCollection();
            foreach (string key in queryParams)
                query[key] = queryParams[key];
            uriBuilder.Query = query.ToString();
        }

        return new FileDownloadRes(uriBuilder.ToString());
    }
}