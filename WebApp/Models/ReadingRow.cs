using Entities.Models;
using Entities.Models.Enum;
using System.Web.Mvc.Html;

namespace WebApp.Models;

public class ReadingRow(Reading reading)
{
    public string Type { get; set; } = EnumHelper.GetSelectList(typeof(ReadingEnum)).First(e => e.Value == ((int)reading.ReadingEnum).ToString()).Text;
    public Guid Id { get; set; } = reading.Id;
    public string Name { get; set; } = reading.Name;
    public string Date { get; set; } = reading.Date.ToString("dd/MM/yyyy");
}