using DataLayer.Models.AbstractEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("GuidedMeditations")]
public class GuidedMeditation : Entity
{
    public GuidedMeditation()
    {
        Description = string.Empty;
    }

    public bool Active { get; set; }

    [StringLength(2000)]
    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [StringLength(500)]
    public string? FileUrl { get; set; }
}