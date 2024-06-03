using System.Collections.Specialized;

namespace BusinessLayer.Services.DTOs.FileServiceDTOs;
public class FileDownloadReqOptions : Supabase.Storage.TransformOptions
{
    private new string Format = "origin";

    public FileDownloadReqOptions(int width, int height, int resize, int quality) : base()
    {
        Width = width;
        Height = height;
        Resize = (ResizeType) resize;
        Quality = quality;
    }

    public FileDownloadReqOptions(){
        base.Format = this.Format;
    }

    public static FileDownloadReqOptions Initialize() =>
        new FileDownloadReqOptions(720, 1280, 1, 80);
    
    public static FileDownloadReqOptions InitializeFromQueryParams(
        int width,
        int height,
        int resize,
        int quality
    ) =>
        new FileDownloadReqOptions(width, height, resize, quality);

    public string ToQueryParams() => $"?width={Width}&height={Height}&resize={Resize}&format={Format}&quality={Quality}";
    public NameValueCollection ToNameValueCollection() =>
        new NameValueCollection
        {
            { "width", Width.ToString() },
            { "height", Height.ToString() },
            { "resize", Resize.ToString() },
            { "format", Format },
            { "quality", Quality.ToString() }
        };
    }