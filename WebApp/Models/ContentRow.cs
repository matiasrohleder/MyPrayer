using Entities.Models;

namespace WebApp.Models;

public class ContentRow
{
    public bool Active { get; set; }
    public string Category { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShowDate { get; set; }

    public ContentRow()
    {
    }

    public ContentRow(Content content)
    {
        Active = content.Active;
        Category = content.Category.Name;
        Id = content.Id;
        Name = content.Name;
        ShowDate = content.ShowDate.ToString("dd/MM/yyyy");
    }
}