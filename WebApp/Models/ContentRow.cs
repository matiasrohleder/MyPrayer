using Entities.Models;

namespace WebApp.Models;

public class ContentRow(Content content)
{
    public bool Active { get; set; } = content.Active;
    public string Category { get; set; } = content.Category!.Name;
    public Guid Id { get; set; } = content.Id;
    public string Name { get; set; } = content.Name;
    public string ShowDate { get; set; } = content.ShowDate.ToString("dd/MM/yyyy");
}