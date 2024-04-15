using Entities.Models;
using Entities.Models.Enum;

namespace WebAPI.DTOs
{
    public class ReadingRes
    {
        public DateTime Date { get; set; }
        public Guid Id { get; set; }
        public ReadingEnum Lecture { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        
        public ReadingRes(Reading reading)
        {
            Date = reading.Date;
            Id = reading.Id;
            Lecture = reading.ReadingEnum;
            Text = reading.Text;
            Title = reading.Name;
        }
    }
}