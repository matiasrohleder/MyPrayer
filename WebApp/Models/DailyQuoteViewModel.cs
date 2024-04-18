using Entities.Models;

namespace WebApp.Models;

public class DailyQuoteViewModel
{
    public DateTime Date { get; set; }
    public Guid Id { get; set; }
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