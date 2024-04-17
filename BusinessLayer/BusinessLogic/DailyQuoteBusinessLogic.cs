using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BusinessLayer.BusinessLogic
{
    public class DailyQuoteBusinessLogic : IDailyQuoteBusinessLogic
    {
        #region Properties
        private readonly IService<DailyQuote> dailyQuoteService;
        #endregion

        #region Constructor
        public DailyQuoteBusinessLogic(IService<DailyQuote> dailyQuoteService)
        {
            this.dailyQuoteService = dailyQuoteService;
        }
        #endregion

        public async Task ValidateQuote(DailyQuote dailyQuote, ModelStateDictionary modelState)
        {
            bool existingQuote = dailyQuoteService.GetAll().Any(q => q.Date.ToUniversalTime().Date == dailyQuote.Date.ToUniversalTime().Date && q.Id != dailyQuote.Id);

            if (existingQuote)
            {
                modelState.AddModelError("date", "Ya existe una frase diaria para el día ingresado");
            }
        }
    }
}