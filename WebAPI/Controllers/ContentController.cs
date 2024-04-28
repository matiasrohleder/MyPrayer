using BusinessLayer.Interfaces;
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
    [Authorize]
    public class ContentController : Controller
    {
        private readonly IService<Content> contentService;
        private readonly IFileService fileService;

        public ContentController(IService<Content> contentService, IFileService fileService)
        {
            this.contentService = contentService;
            this.fileService = fileService;
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
            // Build public URLs for files
            foreach (var carousel in recents){
                foreach (var item in carousel.Contents){
                    item.Image = (await fileService.GetSignedURLAsync(item.Image)).SignedUrl;
                }
            }

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
            // Build public URLs for files
            foreach (var item in contents){
                item.Image = (await fileService.GetSignedURLAsync(item.Image)).SignedUrl;
            }

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
            ContentRes response = new ContentRes(content);
            // Build public URLs for file
            response.Image = (await fileService.GetSignedURLAsync(response.Image)).SignedUrl;
            return Ok(response);
        }
    }
}