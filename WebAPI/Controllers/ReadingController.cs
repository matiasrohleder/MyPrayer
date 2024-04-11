using DataLayer.Interfaces;
using Entities.Constants.Authentication;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tools.WebTools.Attributes;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/reading")]
    [ApiController]
    [AuthorizeAnyRoles(Roles.Admin, Roles.ReadingAdmin)]
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
        public async Task<ActionResult<List<ReadingRes>>> GetDailyReadings(DateTime date)
        {
            List<ReadingRes> dailyReadings = (await readingService.GetAll()
                                                            .Where(r => !r.Deleted && r.Date.Date == date.Date)
                                                            .Select(r => new ReadingRes(r))
                                                            .ToListAsync())
                                                            .OrderBy(r => r.ReadingEnum)
                                                            .ToList();

            return dailyReadings.Any() ? Ok(dailyReadings) : BadRequest("No hay lecturas para el día ingresado");
        }
    }
}