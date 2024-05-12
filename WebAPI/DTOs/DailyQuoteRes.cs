using Entities.Models;
namespace WebAPI.DTOs
{
    public class DailyQuoteRes
    {
        public DateTime Date { get; set; }
        public string Quote { get; set; }
        
        public DailyQuoteRes(DailyQuote dailyQuote)
        {
            Date = dailyQuote.Date;
            Quote = dailyQuote.Name;
        }
    }
}