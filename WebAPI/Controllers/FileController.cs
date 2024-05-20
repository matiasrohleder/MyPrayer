using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("file")]
    [Authorize]
    public class FileController : Controller
    {
        private readonly IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<FileUploadRes> UploadAsync(IFormFile file)
        {
			return await this.fileService.UploadAsync(file);
        }

        [HttpGet("signed-url")]
        public async Task<FileDownloadRes> GetURLAsync(string fileName)
        {
			return await this.fileService.GetSignedURLAsync(fileName);
        }
    }
}