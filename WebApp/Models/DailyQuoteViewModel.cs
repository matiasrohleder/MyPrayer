using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class DailyQuoteViewModel
{
    [Required(ErrorMessage = "La fecha es requerida")]
    public DateTime Date { get; set; }

    public Guid Id { get; set; }

    [Required(ErrorMessage = "La frase diaria es requerida")]
    public string Name { get; set; }

    public DailyQuoteViewModel()
    {
        Date = DateTime.Today.AddDays(1);
        Id = Guid.NewGuid();
        Name = string.Empty;
    }

    public DailyQuoteViewModel(DailyQuote dailyQuote)
    {
        Date = dailyQuote.Date;
        Id = dailyQuote.Id;
        Name = dailyQuote.Name;
    }

    public DailyQuote ToEntity()
    {
        DailyQuote dailyQuote = new()
        {
            Date = Date.ToUniversalTime(),
            Id = Id,
            Name = Name
        };

        return dailyQuote;
    }
}