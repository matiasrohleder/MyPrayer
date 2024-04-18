using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/content")]
    [ApiController]
    public class ContentController : Controller
    {
        private readonly IService<Content> contentService;

        public ContentController(IService<Content> contentService)
        {
            this.contentService = contentService;
        }

        /// <summary>
        /// Get recent contents grouped by category with param amount for each category
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpGet("recent")]
        public async Task<ActionResult<List<RecentContentItem>>> GetRecent(int amount = 5)
        {
            if (amount <= 0)
                return BadRequest("La cantidad debe ser un número positivo mayor a 0.");

            List<RecentContentItem> recents = (await contentService.GetAll()
                                                            .Include(c => c.Category)
                                                            .Where(c => !c.Deleted && c.Active && c.ShowDate <= DateTime.Now.ToUniversalTime())
                                                            .GroupBy(c => c.CategoryId)
                                                            .Select(c => new RecentContentItem(c.First().Category)
                                                            {
                                                                Contents = c.OrderByDescending(i => i.ShowDate).Take(amount).Select(i => new ContentRes(i)).ToList()
                                                            })
                                                            .ToListAsync())
                                                            .OrderBy(c => c.Category.Order)
                                                            .ToList();

            return Ok(recents);
        }

        /// <summary>
        /// Get all contents by category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<ContentRes>>> GetByCategory(Guid categoryId)
        {
            List<ContentRes> contents = await contentService.GetAll()
                                                            .Where(c => !c.Deleted && c.Active && c.ShowDate <= DateTime.Now.ToUniversalTime() && c.CategoryId == categoryId)
                                                            .OrderByDescending(c => c.ShowDate)
                                                            .Select(c => new ContentRes(c))
                                                            .ToListAsync();

            return Ok(contents);
        }

        /// <summary>
        /// Get content by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ContentRes>> Get(Guid id)
        {
            Content content = await contentService.GetAsync(id);

            return Ok(new ContentRes(content));
        }
    }
}