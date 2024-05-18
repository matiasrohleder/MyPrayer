using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration configuration;
        private string apiKey => this.configuration.GetSection("FileService:APIKey")?.Value ?? throw new Exception("File Service API key must have a value");
        private string baseURL => this.configuration.GetSection("FileService:BaseURL")?.Value ?? throw new Exception("File Service BaseURL must have a value");
        private string imageBucket => this.configuration.GetSection("FileService:ImageBucket")?.Value ?? throw new Exception("File Service ImageBucket must have a value");
        private string audioBucket => this.configuration.GetSection("FileService:AudioBucket")?.Value ?? throw new Exception("File Service AudioBucket must have a value");
        private string behaviourOnBucketMissing => this.configuration.GetSection("FileService:BehaviourOnBucketMissing")?.Value ?? throw new Exception("File Service BehaviourOnBucketMissing must have a value");

        private async Task<Supabase.Storage.Interfaces.IStorageFileApi<Supabase.Storage.FileObject>> GetStorage(string bucket = "TEST")
        {
            var url = this.baseURL;
            var key = this.apiKey;

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = false
            };

            var supabase = new Supabase.Client(url, key, options);
            await supabase.InitializeAsync();

            var bucketList = await supabase.Storage.ListBuckets();
            if(bucketList is null || bucketList.Count == 0 || !bucketList.Any(bl => bl.Name == bucket))
            {
                if(!string.IsNullOrEmpty(behaviourOnBucketMissing) && behaviourOnBucketMissing == "Create")
                    await supabase.Storage.CreateBucket(bucket);
                else // TODO: Explicitly check for "Exception" or "Throw" string constant value. Also group these possible values in an Enum or static class.
                    throw new Exception($"There is no bucket named {bucket}. Please, make sure  your Supabase instance is configured with a {bucket} Bucket or check if the spelling is correct in the appsettings or environment variables for this configuration.");
            }

            return supabase.Storage.From(bucket);
        }

        public FileService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc />
        public async Task<FileUploadRes> UploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("No file uploaded.");
            
            var storage = file.ContentType.Contains("image") ? await GetStorage(imageBucket)
                        : file.ContentType.Contains("audio") ? await GetStorage(audioBucket)
                                                             : await GetStorage();

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
        public async Task<FileDownloadRes> GetSignedURLAsync(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out string contentType))
                contentType = "";

            var storage = contentType.Contains("image") ? await GetStorage(imageBucket)
                        : contentType.Contains("audio") ? await GetStorage(audioBucket)
                                                             : await GetStorage();

            string result = "";
            var list = await storage.List(options: new Supabase.Storage.SearchOptions(){
                Search = fileName
            });
            if(list != null && list.Count > 0 && list.Any(f => f.Name == fileName))
                // Generate a signed URL valid for 60 minutes
                result = await storage.CreateSignedUrl(fileName, 60 * 60);

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