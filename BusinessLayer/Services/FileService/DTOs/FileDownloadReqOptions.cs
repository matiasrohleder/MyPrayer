using System.Collections.Specialized;
using Supabase.Storage;

namespace BusinessLayer.Services.FileService;
/// <summary>
/// Object to pass options when requesting a download URL for a media file.
/// This options give directions on how the media file should be transformed
/// so as to optimize it for the current use case.
/// </summary>
public class FileDownloadReqOptions : TransformOptions
{
    /// <summary>
    /// Format should never change. Hide provider property from API clients.
    /// </summary>
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