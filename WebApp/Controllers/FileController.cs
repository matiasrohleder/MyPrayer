using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;

namespace WebApp.Controllers;

[Authorize]
public class FileController : Controller
{
    private readonly IFileService fileService;

    public FileController(IFileService fileService)
    {
        this.fileService = fileService;
    }

    public async Task Upload(IFormFile file)
    {
        await this.fileService.UploadAsync(file);
    }

    public async Task<byte[]> Download(string fileName)
    {
        return await this.fileService.DownloadAsync(fileName);
    }
}