using Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BusinessLayer.Interfaces
{
    public interface IDailyQuoteBusinessLogic
    {
        /// <summary>
        /// Validate quote with quotes in DB.
        /// </summary>
        Task ValidateQuote(DailyQuote dailyQuote, ModelStateDictionary modelState);
    }
}
