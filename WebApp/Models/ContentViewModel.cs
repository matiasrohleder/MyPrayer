using Entities.Models;

namespace WebApp.Models;

public class ContentViewModel
{
    public bool Active { get; set; }
    public Guid CategoryId { get; set; }
    public string Description { get; set; }
    public Guid Id { get; set; }
    public string Link { get; set; }
    public string Name { get; set; }
    public DateTime ShowDate { get; set; }
    public string? File { get; set; }
    public string? FileUrl { get; set; }
    public string? SignedUrl { get; set; }

    public ContentViewModel()
    {
        Active = true;
        Description = string.Empty;
        Id = Guid.NewGuid();
        Link = string.Empty;
        Name = string.Empty;
        ShowDate = DateTime.Today;
    }

    public ContentViewModel(Content content)
    {
        Active = content.Active;
        CategoryId = content.CategoryId;
        Description = content.Description;
        Id = content.Id;
        Link = content.Link;
        Name = content.Name;
        ShowDate = content.ShowDate;
        FileUrl = content.FileUrl;
    }

    public Content ToEntity()
    {
        Content content = new()
        {
            Active = Active,
            CategoryId = CategoryId,
            Description = Description,
            Id = Id,
            Link = Link,
            Name = Name,
            ShowDate = ShowDate.ToUniversalTime(),
            FileUrl = FileUrl
        };

        return content;
    }
}