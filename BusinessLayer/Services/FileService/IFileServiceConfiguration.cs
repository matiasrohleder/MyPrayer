
namespace BusinessLayer.Services.FileService;

public interface IFileServiceConfiguration
{
    string BaseURL { get; set; }
    string StorageResource { get; set; }
    string Bucket { get; set; }
    string APIKey { get; set; }
    string ImageBucket { get; set; }
    string AudioBucket { get; set; }
    string BehaviourOnBucketMissing { get; set; }
}