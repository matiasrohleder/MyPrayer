using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory clientFactory;
        private readonly string apiKey;

        public FileService(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
        this.configuration = configuration;
        this.clientFactory = clientFactory;
        this.apiKey = configuration.GetSection("FileService:APIKey").Value ?? throw new Exception("File Service API key must have a value");
        }

        /// <inheritdoc />
        public async Task UploadAsync(IFormFile file)
        {
            using (var client = clientFactory.CreateClient())
            {
                string endpoint = GetEndpoint(file.FileName);
                var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                AddAuthorization(request);
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(file.OpenReadStream())
                    {
                        Headers =
                        {
                            ContentLength = file.Length,
                            ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType)
                        }
                    }, "file", file.FileName);

                    request.Content = content;
                    var response = await client.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Failed to upload file");
                    }
                }  
            }
        }

        /// <inheritdoc />
        public async Task<byte[]> DownloadAsync(string fileName)
        {
            using (var client = clientFactory.CreateClient())
            {
                string endpoint = GetEndpoint(fileName);
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                AddAuthorization(request);
                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to download file");
                }

                return await response.Content.ReadAsByteArrayAsync();
            }
        }

        private string GetEndpoint(string fileName)
        {
            string baseURL = configuration.GetSection("FileService:BaseURL").Value ?? throw new Exception("File Service BaseURL must have a value");
            string action = configuration.GetSection("FileService:URL").Value ?? throw new Exception("File Service URL must have a value");
            string bucket = configuration.GetSection("FileService:Bucket").Value ?? throw new Exception("File Service Bucket must have a value");
            return string.Format(baseURL + action, bucket, fileName);
        }

        private void AddAuthorization(HttpRequestMessage request) => request.Headers.Add("Authorization", $"Bearer {apiKey}");
    }
}