using DataLayer.Models.AbstractEntities;
using Entities.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Reading : Entity
{
    public Reading()
    {
        Text = string.Empty;
    }

    public DateTime Date { get; set; }

    [StringLength(65000)]
    public string Text { get; set; }
    
    public ReadingEnum ReadingEnum { get; set; }
}