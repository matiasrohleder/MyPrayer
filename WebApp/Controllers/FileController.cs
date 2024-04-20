using Microsoft.AspNetCore.Authorization;
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

    public async Task<FileDownloadRes> SignedURL(string fileName)
    {
        return await this.fileService.GetSignedURLAsync(fileName);
    }
}