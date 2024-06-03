namespace BusinessLayer.Services.DTOs.FileServiceDTOs;

/// <summary>
/// Object returned when retrieving a file.
/// </summary>
public class FileDownloadRes
{
    /// <summary>
    /// Publicly accesible file url.
    /// </summary>
    public string? PublicUrl { get; set; }

    public FileDownloadRes(string publicUrl)
    {
        this.PublicUrl = publicUrl;
    }
}