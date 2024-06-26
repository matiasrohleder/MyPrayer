using BusinessLayer.Services.FileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("public-url")]
        public FileDownloadRes GetPublicURL(
            string fileName,
            int width = 720,
            int height = 1280,
            int resize = 1,
            int quality = 80)
        {
			return this.fileService.GetPublicURL(fileName, new FileDownloadReqOptions(width, height, resize, quality));
        }
    }
}