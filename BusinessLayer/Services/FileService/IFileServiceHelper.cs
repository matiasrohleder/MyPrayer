using Supabase.Storage;
using Supabase.Storage.Interfaces;

namespace BusinessLayer.Services.FileService;

/// <summary>
/// Handle methods that have nothing to do with the logic of uploading or retrieving a file. I.E. Handle provider specifics.
/// </summary>
public interface IFileServiceHelper
{
    /// <summary>
    /// Get coresponding bucket for the given content type
    /// </summary>
    string GetBucket(string contentType);

    /// <summary>
    /// Get coresponding storage instance for the given content type
    /// </summary>
    Task<IStorageFileApi<FileObject>> GetStorage(string contentType);

    /// <summary>
    /// Get the public URL that should be used to acces a given file according to the provider configuration
    /// </summary>
    UriBuilder GetUriForfile(string fileName);
}