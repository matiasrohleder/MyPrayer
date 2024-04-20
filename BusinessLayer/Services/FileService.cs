using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory clientFactory;
        private string apiKey => configuration.GetSection("FileService:APIKey").Value ?? throw new Exception("File Service API key must have a value");
        private string baseURL => this.configuration.GetSection("FileService:BaseURL").Value ?? throw new Exception("File Service BaseURL must have a value");

        private async Task<Supabase.Storage.Interfaces.IStorageFileApi<Supabase.Storage.FileObject>> GetStorage()
        {
            var url = this.baseURL;
            var key = this.apiKey;

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = false
            };

            var supabase = new Supabase.Client(url, key, options);
            await supabase.InitializeAsync();

            return supabase.Storage.From("TEST");
        }

        public FileService(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            this.configuration = configuration;
            this.clientFactory = clientFactory;
        }

        /// <inheritdoc />
        public async Task<FileUploadRes> UploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("No file uploaded.");

            var storage = await GetStorage();
            
            // Convert IFormFile to byte array
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            // Create a unique file name to prevent overwriting existing files (optional)
            var now = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            string fileName = $"{now}-{file.FileName}";

            // Upload the file
            string fileUrl = await storage.Upload(fileBytes, fileName);

            return new FileUploadRes { FileUrl = fileName };
        }

        /// <inheritdoc />
        public async Task<FileDownloadRes> GetSignedURLAsync(string fileName)
        {
            var storage = await GetStorage();

            // Generate a signed URL valid for 60 minutes
            var result = await storage.CreateSignedUrl(fileName, 60 * 60);

            return new FileDownloadRes { SignedUrl = result };
        }
    }

    public class FileUploadRes
    {
        public string FileUrl { get; set; }
    }

    public class FileDownloadRes
    {
        public string SignedUrl { get; set; }
    }
}