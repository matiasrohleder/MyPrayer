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

        [HttpGet("getRecent")]
        public IActionResult GetRecent(int amount = 5)
        {
            List<RecentContentItem> recents = contentService.GetAll()
                                                            .Include(c => c.Category)
                                                            .Where(c => !c.Deleted && c.Active && c.ShowDate <= DateTime.Now)
                                                            .GroupBy(c => c.CategoryId)
                                                            .Select(c => new RecentContentItem(c.First().Category)
                                                            {
                                                                Contents = c.OrderByDescending(i => i.ShowDate).Take(amount).Select(i => new ContentDTO(i)).ToList()
                                                            })
                                                            .ToList()
                                                            .OrderBy(c => c.Order)
                                                            .ToList();

            return Ok(recents);
        }

        [HttpGet("getByCategory")]
        public IActionResult GetByCategory(Guid categoryId)
        {
            List<ContentDTO> contents = contentService.GetAll()
                                                            .Where(c => !c.Deleted && c.Active && c.ShowDate <= DateTime.Now && c.CategoryId == categoryId)
                                                            .OrderByDescending(c => c.ShowDate)
                                                            .Select(c => new ContentDTO(c))
                                                            .ToList();

            return Ok(contents);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(Guid id)
        {
            Content content = await contentService.GetAsync(id);

            return Ok(new ContentDTO(content));
        }
    }
}