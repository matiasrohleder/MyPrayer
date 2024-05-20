using Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BusinessLayer.Interfaces
{
    public interface IReadingBusinessLogic
    {
        /// <summary>
        /// Get daily bible readings and store in DB.
        /// </summary>
        Task GetReadings();

        /// <summary>
        /// Validate reading with readings in DB.
        /// </summary>
        Task ValidateReading(Reading reading, ModelStateDictionary modelState);
    }
}
