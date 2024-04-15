using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/dailyQuote")]
    [ApiController]
    [Authorize]
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
        [HttpGet("get")]
        public async Task<ActionResult<List<DailyQuoteRes>>> GetByDate(DateTime date)
        {
            DailyQuoteRes? quote = await dailyQuoteService.GetAll()
                                                            .Where(r => !r.Deleted && r.Date.ToUniversalTime().Date == date.ToUniversalTime().Date)
                                                            .Select(r => new DailyQuoteRes(r))
                                                            .FirstOrDefaultAsync();

            return quote != null ? Ok(quote) : BadRequest("No hay frase diaria para el día ingresado");
        }
    }
}