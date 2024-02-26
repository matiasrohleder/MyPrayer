using System.ComponentModel.DataAnnotations;

namespace Entities.Models.Enum
{
    public enum ReadingEnum
    {
        [Display(Name = "Primera Lectura")]
        FirstReading = 0,
        [Display(Name = "Salmo")]
        Psalm = 1,
        [Display(Name = "Segunda Lectura")]
        SecondReading = 2,
        [Display(Name = "Evangelio")]
        Gospel = 3,
    }
}