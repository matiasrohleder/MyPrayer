using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services.FileService;

namespace WebApp.Controllers;

public class FileController : Controller
{
    private readonly IFileService fileService;

    public FileController(IFileService fileService)
    {
        this.fileService = fileService;
    }

    public async Task<FileUploadRes> Upload(IFormFile file)
    {
        return await this.fileService.UploadAsync(file);
    }

    public FileDownloadRes PublicURL(
            string fileName,
            int width = 720,
            int height = 1280,
            int resize = 1,
            int quality = 80)
    {
        return this.fileService.GetPublicURL(fileName, new FileDownloadReqOptions(width, height, resize, quality));
    }
}