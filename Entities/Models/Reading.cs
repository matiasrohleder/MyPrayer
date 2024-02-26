using DataLayer.Models.AbstractEntities;
using Entities.Models.Enum;

namespace Entities.Models;

public class Reading : Entity
{
    public Reading()
    {
    }

    public DateTime Date { get; set; }

    public string Text { get; set; }
    
    public ReadingEnum ReadingEnum { get; set; }
}