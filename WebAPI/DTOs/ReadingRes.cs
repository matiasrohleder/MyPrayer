using Entities.Models;
using Entities.Models.Enum;

namespace WebAPI.DTOs
{
    public class ReadingRes
    {
        public DateTime Date { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ReadingEnum ReadingEnum { get; set; }
        public string Text { get; set; }
        
        public ReadingRes(Reading reading)
        {
            Date = reading.Date;
            Id = reading.Id;
            Name = reading.Name;
            ReadingEnum = reading.ReadingEnum;
            Text = reading.Text;
        }
    }
}