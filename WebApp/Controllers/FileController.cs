using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;

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

    public async Task<FileDownloadRes> SignedURL(
            string fileName,
            int width = 720,
            int height = 1280,
            int resize = 1,
            int quality = 80)
    {
        return await this.fileService.GetSignedURLAsync(fileName, FileDownloadReqOptions.InitializeFromQueryParams(width, height, resize, quality));
    }

    public FileDownloadRes PublicURL(
            string fileName,
            int width = 720,
            int height = 1280,
            int resize = 1,
            int quality = 80)
    {
        return this.fileService.GetPublicURL(fileName, FileDownloadReqOptions.InitializeFromQueryParams(width, height, resize, quality));
    }
}