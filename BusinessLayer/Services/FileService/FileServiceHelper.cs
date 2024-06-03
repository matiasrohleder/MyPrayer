using Microsoft.AspNetCore.StaticFiles;
using Supabase.Storage;
using Supabase.Storage.Interfaces;

namespace BusinessLayer.Services.FileService;

/// <summary>
/// Handle methods that have nothing to do with the logic of uploading or retrieving a file. I.E. Handle provider specifics.
/// </summary>
internal class FileServiceHelper : IFileServiceHelper
{
    private readonly IFileServiceConfiguration fileServiceConfiguration;
    public FileServiceHelper(IFileServiceConfiguration fileServiceConfiguration)
    {
        this.fileServiceConfiguration = fileServiceConfiguration;
    }

    /// <summary>
    /// Get coresponding bucket for the given content type
    /// </summary>
    public string GetBucket(string contentType){
        return contentType.Contains("image") ? fileServiceConfiguration.ImageBucket
                : contentType.Contains("audio") ? fileServiceConfiguration.AudioBucket
                                                : fileServiceConfiguration.Bucket; 
    }

    /// <summary>
    /// Get coresponding storage instance for the given content type
    /// </summary>
    public async Task<IStorageFileApi<FileObject>> GetStorage(string contentType)
    {
        string bucket = GetBucket(contentType);

        var url = this.fileServiceConfiguration.BaseURL;
        var key = this.fileServiceConfiguration.APIKey;

        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = false
        };

        var supabase = new Supabase.Client(url, key, options);
        await supabase.InitializeAsync();

        var bucketList = await supabase.Storage.ListBuckets();
        if(bucketList is null || bucketList.Count == 0 || !bucketList.Any(bl => bl.Name == bucket))
        {
            if(!string.IsNullOrEmpty(fileServiceConfiguration.BehaviourOnBucketMissing) && fileServiceConfiguration.BehaviourOnBucketMissing == "Create")
                await supabase.Storage.CreateBucket(bucket);
            else // TODO: Explicitly check for "Exception" or "Throw" string constant value. Also group these possible values in an Enum or static class.
                throw new Exception($"There is no bucket named {bucket}. Please, make sure  your Supabase instance is configured with a {bucket} Bucket or check if the spelling is correct in the appsettings or environment variables for this configuration.");
        }

        return supabase.Storage.From(bucket);
    }

    /// <summary>
    /// Get the public URL that should be used to acces a given file according to the provider configuration
    /// </summary>
    public UriBuilder GetUriForfile(string fileName){

        var provider = new FileExtensionContentTypeProvider();

        if (!provider.TryGetContentType(fileName, out string contentType))
            contentType = "";

        string bucket = GetBucket(contentType);

        return new UriBuilder(this.fileServiceConfiguration.BaseURL)
        {
            Path = string.Format(this.fileServiceConfiguration.StorageResource, bucket, fileName)
        };
    }
}