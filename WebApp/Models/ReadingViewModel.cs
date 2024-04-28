using Entities.Models;
using Entities.Models.Enum;

namespace WebApp.Models;

public class ReadingViewModel
{
    public DateTime Date { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ReadingEnum ReadingEnum { get; set; }
    public string Text { get; set; }

    public ReadingViewModel()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Text = string.Empty;
    }

    public ReadingViewModel(Reading reading)
    {
        Date = reading.Date;
        Id = reading.Id;
        Name = reading.Name;
        ReadingEnum = reading.ReadingEnum;
        Text = reading.Text;
    }

    public Reading ToEntity()
    {
        Reading reading = new()
        {
            Date = Date.ToUniversalTime(),
            Id = Id,
            Name = Name,
            ReadingEnum = ReadingEnum,
            Text = Text
        };

        return reading;
    }
}