using Entities.Models;
namespace WebAPI.DTOs
{
    public class DailyQuoteRes
    {
        public DateTime Date { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public DailyQuoteRes(DailyQuote dailyQuote)
        {
            Date = dailyQuote.Date;
            Id = dailyQuote.Id;
            Name = dailyQuote.Name;
        }
    }
}