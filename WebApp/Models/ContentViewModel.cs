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

    public ContentViewModel()
    {
        Id = Guid.NewGuid();
        Active = true;
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
    }

    public Content ToEntity()
    {
        Content content = new Content
        {
            Active = Active,
            CategoryId = CategoryId,
            Description = Description,
            Id = Id,
            Link = Link,
            Name = Name,
            ShowDate = ShowDate
        };

        return content;
    }
}