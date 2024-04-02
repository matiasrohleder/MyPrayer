using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("file")]
    public class FileController : Controller
    {
        private readonly IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task UploadAsync(IFormFile file)
        {
			await this.fileService.UploadAsync(file);
        }

        [HttpGet("download")]
        public async Task<byte[]> DownloadAsync(string fileName)
        {
			return await this.fileService.DownloadAsync(fileName);
        }
    }
}