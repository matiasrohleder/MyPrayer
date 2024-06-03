namespace BusinessLayer.Services.DTOs.FileServiceDTOs;
public class FileDownloadRes
{
    public string? PublicUrl { get; set; }

    public FileDownloadRes(string publicUrl)
    {
        this.PublicUrl = publicUrl;
    }
}