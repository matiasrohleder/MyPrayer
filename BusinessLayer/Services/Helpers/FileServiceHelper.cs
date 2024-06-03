using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Supabase.Storage;
using Supabase.Storage.Interfaces;

namespace BusinessLayer.Services.Helpers;

/// <summary>
/// Handle methods that have nothing to do with the logic of uploading or retrieving a file. I.E. Handle provider specifics.
/// </summary>
internal class FileServiceHelper
{
    public string apiKey;
    public string baseURL;
    public string storageResource;
    public string imageBucket;
    public string audioBucket;
    public string defaultBucket;
    public string behaviourOnBucketMissing;
    public FileServiceHelper(IConfiguration configuration)
    {
        apiKey = configuration.GetSection("FileService:APIKey")?.Value ?? throw new Exception("File Service API key must have a value");
        baseURL = configuration.GetSection("FileService:BaseURL")?.Value ?? throw new Exception("File Service BaseURL must have a value");
        storageResource = configuration.GetSection("FileService:StorageResource")?.Value ?? throw new Exception("File Service StorageResource must have a value");
        imageBucket = configuration.GetSection("FileService:ImageBucket")?.Value ?? throw new Exception("File Service ImageBucket must have a value");
        audioBucket = configuration.GetSection("FileService:AudioBucket")?.Value ?? throw new Exception("File Service AudioBucket must have a value");
        defaultBucket = configuration.GetSection("FileService:Bucket")?.Value ?? throw new Exception("File Service Bucket must have a value");
        behaviourOnBucketMissing = configuration.GetSection("FileService:BehaviourOnBucketMissing")?.Value ?? throw new Exception("File Service BehaviourOnBucketMissing must have a value");        
    }

    /// <summary>
    /// Get coresponding bucket for the given content type
    /// </summary>
    public string GetBucket(string contentType){
        return contentType.Contains("image") ? imageBucket
                : contentType.Contains("audio") ? audioBucket
                                                : defaultBucket; 
    }

    /// <summary>
    /// Get coresponding storage instance for the given content type
    /// </summary>
    public async Task<IStorageFileApi<FileObject>> GetStorage(string contentType)
    {
        string bucket = GetBucket(contentType);

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

    public UriBuilder GetUriForfile(string fileName){

        var provider = new FileExtensionContentTypeProvider();

        if (!provider.TryGetContentType(fileName, out string contentType))
            contentType = "";

        string bucket = GetBucket(contentType);

        return new UriBuilder(this.baseURL)
        {
            Path = string.Format(this.storageResource, bucket, fileName)
        };
    }
}