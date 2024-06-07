using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services.FileService;

internal class FileServiceConfiguration : IFileServiceConfiguration
{
    public string BaseURL { get; set; }
    public string StorageResource { get; set; }
    public string Bucket { get; set; }
    public string APIKey { get; set; }
    public string ImageBucket { get; set; }
    public string AudioBucket { get; set; }
    public string BehaviourOnBucketMissing { get; set; }

    public FileServiceConfiguration()
    {
        
    }

    public FileServiceConfiguration(IConfiguration configuration)
    {
        Bind(configuration);
    }

    public FileServiceConfiguration Bind(IConfiguration configuration)
    {
        configuration.GetSection("FileService").Bind(this);
        return this;
    }
}