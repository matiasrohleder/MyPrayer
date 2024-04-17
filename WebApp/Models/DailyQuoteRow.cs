using Entities.Models;

namespace WebApp.Models;

public class DailyQuoteRow(DailyQuote dailyQuote)
{
    public string Date { get; set; } = dailyQuote.Date.ToString("dd/MM/yyyy");
    public Guid Id { get; set; } = dailyQuote.Id;
    public string Name { get; set; } = dailyQuote.Name;
}