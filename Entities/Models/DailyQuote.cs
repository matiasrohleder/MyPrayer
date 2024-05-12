using DataLayer.Models.AbstractEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("DailyQuotes")]
public class DailyQuote : Entity
{
    public DailyQuote()
    {
    }

    public DateTime Date { get; set; }
}