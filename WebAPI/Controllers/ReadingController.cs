using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/reading")]
    [ApiController]
    [Authorize]
    public class ReadingController : Controller
    {
        private readonly IService<Reading> readingService;

        public ReadingController(IService<Reading> readingService)
        {
            this.readingService = readingService;
        }

        /// <summary>
        /// Get all daily readings for a specific date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("daily")]
        public async Task<ActionResult<List<ReadingRes>>> GetDailyReadings(DateTime? date)
        {
            if (!date.HasValue)
                date = DateTime.Today;

            List<ReadingRes> dailyReadings = (await readingService.GetAll()
                                                            .Where(r => !r.Deleted && r.Date.ToUniversalTime().Date == date.Value.ToUniversalTime().Date)
                                                            .Select(r => new ReadingRes(r))
                                                            .ToListAsync())
                                                            .OrderBy(r => r.Lecture)
                                                            .ToList();

            return dailyReadings.Any() ? Ok(dailyReadings) : BadRequest("No hay lecturas para el día ingresado");
        }
    }
}