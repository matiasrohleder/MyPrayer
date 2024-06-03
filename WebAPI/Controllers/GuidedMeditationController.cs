using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/guided-meditation")]
    [ApiController]
    [Authorize]
    public class GuidedMeditationController : Controller
    {
        private readonly IService<GuidedMeditation> guidedMeditationService;
        private readonly IFileService fileService;

        public GuidedMeditationController(IService<GuidedMeditation> guidedMeditationService, IFileService fileService)
        {
            this.guidedMeditationService = guidedMeditationService;
            this.fileService = fileService;
        }

        /// <summary>
        /// Get all contents by category id
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("current")]
        public async Task<ActionResult<GuidedMeditationRes>> GetCurrentByDate(DateTime? date)
        {
            if (!date.HasValue)
                date = DateTime.Now;

            GuidedMeditation? guidedMeditation = await guidedMeditationService.GetAll()
                                                                              .FirstOrDefaultAsync(gm => !gm.Deleted && 
                                                                                                         gm.Active && 
                                                                                                         gm.StartDate <= date.Value.ToUniversalTime() && 
                                                                                                         gm.EndDate > date.Value.ToUniversalTime());
            if (guidedMeditation == null)
                return BadRequest("No hay meditaciones guiadas para la fecha.");
            
            GuidedMeditationRes guidedMeditationRes = new(guidedMeditation);

            // Build public URLs for files
            if (!string.IsNullOrEmpty(guidedMeditation.FileUrl))
                guidedMeditationRes.Audio = (await fileService.GetSignedURLAsync(guidedMeditationRes.Audio)).SignedUrl;

            return Ok(guidedMeditationRes);
        }
    }
}