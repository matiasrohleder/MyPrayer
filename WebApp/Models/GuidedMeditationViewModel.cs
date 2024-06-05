using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class GuidedMeditationViewModel
{
    public bool Active { get; set; }

    public string? Description { get; set; }

    public Guid Id { get; set; }

    [Required(ErrorMessage = "El nombre de la meditaci\xf3n es requerida")]
    public string Name { get; set; }

    [Required(ErrorMessage = "La fecha de inicio es requerida")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "La fecha de fin es requerida")]
    public DateTime EndDate { get; set; }

    public string? File { get; set; }

    [Required(ErrorMessage = "El archivo de audio es requerido")]
    public string? FileUrl { get; set; }

    public string? PublicUrl { get; set; }

    public GuidedMeditationViewModel()
    {
        Active = true;
        Description = string.Empty;
        Id = Guid.NewGuid();
        Name = string.Empty;
        StartDate = DateTime.Today;
        EndDate = DateTime.Today.AddMonths(1);
    }

    public GuidedMeditationViewModel(GuidedMeditation guidedMeditation)
    {
        Active = guidedMeditation.Active;
        Description = guidedMeditation.Description;
        Id = guidedMeditation.Id;
        Name = guidedMeditation.Name;
        StartDate = guidedMeditation.StartDate;
        EndDate = guidedMeditation.EndDate;
        FileUrl = guidedMeditation.FileUrl;
    }

    public GuidedMeditation ToEntity()
    {
        GuidedMeditation guidedMeditation = new()
        {
            Active = Active,
            Description = string.IsNullOrEmpty(Description) ? string.Empty : Description,
            Id = Id,
            Name = Name,
            StartDate = StartDate.ToUniversalTime(),
            EndDate = EndDate.ToUniversalTime(),
            FileUrl = FileUrl
        };

        return guidedMeditation;
    }
}