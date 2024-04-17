using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/daily-quote")]
    [ApiController]
    public class DailyQuoteController : Controller
    {
        private readonly IService<DailyQuote> dailyQuoteService;

        public DailyQuoteController(IService<DailyQuote> dailyQuoteService)
        {
            this.dailyQuoteService = dailyQuoteService;
        }

        /// <summary>
        /// Get daily quote for a specific date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("daily")]
        public async Task<DailyQuoteRes> GetByDate(DateTime? date)
        {
            if (!date.HasValue)
                date = DateTime.Today;

            DailyQuoteRes? quote = await dailyQuoteService.GetAll()
                                                            .Where(r => !r.Deleted && r.Date.ToUniversalTime().Date == date.Value.ToUniversalTime().Date)
                                                            .Select(r => new DailyQuoteRes(r))
                                                            .FirstOrDefaultAsync();

            return quote;
        }
    }
}