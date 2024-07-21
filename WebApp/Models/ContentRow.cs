using Entities.Models;

namespace WebApp.Models;

public class ContentRow(Content content)
{
    public bool Active { get; set; } = content.Active;
    public string Category { get; set; } = content.Category!.Name;
    public Guid Id { get; set; } = content.Id;
    public string Name { get; set; } = content.Name;
    public string DateStart { get; set; } = content.DateStart.ToString("dd/MM/yyyy");
    public string DateEnd { get; set; } = content.DateEnd?.ToString("dd/MM/yyyy");
}