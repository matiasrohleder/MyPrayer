using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : Controller
    {
        private readonly IService<Content> contentService;

        public ContentController(IService<Content> contentService)
        {
            this.contentService = contentService;
        }

        [HttpGet]
        public IActionResult GetRecent(int amount = 5)
        {
            List<RecentContentItem> recents = contentService.GetAll()
                                                            .Include(c => c.Category)
                                                            .Where(c => !c.Deleted && c.Active && c.ShowDate <= DateTime.Now)
                                                            .GroupBy(c => c.CategoryId)
                                                            .Select(c => new RecentContentItem(c.First().Category)
                                                            {
                                                                Contents = c.OrderByDescending(i => i.ShowDate).Take(amount).Select(i => new RecentContentDTO(i)).ToList()
                                                            })
                                                            .ToList()
                                                            .OrderBy(c => c.Order)
                                                            .ToList();

            int count = recents.Count();
            return Ok(new
            {
                Total = count,
                Page = 1,
                PageSize = count,
                Items = recents
            });
        }
    }
}
