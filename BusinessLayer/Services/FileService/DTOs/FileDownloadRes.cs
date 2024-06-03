namespace BusinessLayer.Services.FileService;

/// <summary>
/// Object returned when retrieving a file.
/// </summary>
public class FileDownloadRes
{
    /// <summary>
    /// Publicly accesible file url.
    /// </summary>
    public string? PublicUrl { get; set; }
    public FileDownloadRes()
    {
        
    }
    public FileDownloadRes(string publicUrl)
    {
        this.PublicUrl = publicUrl;
    }
}