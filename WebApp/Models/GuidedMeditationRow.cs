using Entities.Models;

namespace WebApp.Models;

public class GuidedMeditationRow(GuidedMeditation guidedMeditation)
{
    public bool Active { get; set; } = guidedMeditation.Active;
    public Guid Id { get; set; } = guidedMeditation.Id;
    public string Name { get; set; } = guidedMeditation.Name;
    public string StartDate { get; set; } = guidedMeditation.StartDate.ToString("dd/MM/yyyy");
    public string EndDate { get; set; } = guidedMeditation.EndDate.ToString("dd/MM/yyyy");
}