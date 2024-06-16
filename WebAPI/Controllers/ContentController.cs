using BusinessLayer.Services.FileService;
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
        public async Task<ActionResult<List<RecentContentItem>>> GetRecent(
            int amount = 5,
            int width = 720,
            int height = 1280,
            int resize = 1,
            int quality = 80)
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
            var tasks = recents.SelectMany(carousel =>
                    carousel.Contents
                        .Where(c => !string.IsNullOrEmpty(c.Image))
                        .Select(async item =>
                        {
                            item.Image = fileService.GetPublicURL(item.Image!, new FileDownloadReqOptions(width, height, resize, quality))?.PublicUrl;
                        })
                ).ToList();

            await Task.WhenAll(tasks);
            
            return Ok(recents);
        }

        /// <summary>
        /// Get all contents by category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<PaginatedContentRes>> GetByCategory(
            Guid categoryId,
            int skip = 0,
            int take = 5,
            int width = 720,
            int height = 1280,
            int resize = 1,
            int quality = 80)
        {
            List<ContentRes> contents = await contentService.GetAll()
                                                            .Where(c => !c.Deleted && c.Active && c.ShowDate <= DateTime.Now.ToUniversalTime() && c.CategoryId == categoryId)
                                                            .OrderByDescending(c => c.ShowDate)
                                                            .Skip(skip).Take(take)
                                                            .Select(c => new ContentRes(c))
                                                            .ToListAsync();
            // Build public URLs for files
            foreach (var item in contents.Where(c => !string.IsNullOrEmpty(c.Image)))
                item.Image = fileService.GetPublicURL(item.Image!, new FileDownloadReqOptions(width, height, resize, quality))?.PublicUrl;

            // Get total pages
            int totalPages = ((await contentService.GetAll().CountAsync()) / take) + 1;

            return Ok(new PaginatedContentRes(contents, totalPages));
        }

        /// <summary>
        /// Get content by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ContentRes>> Get(
            Guid id,
            int width = 720,
            int height = 1280,
            int resize = 1,
            int quality = 80)
        {
            Content? content = await contentService.GetAsync(id);
            if(content is null)
                return NoContent();

            ContentRes response = new ContentRes(content!);

            // Build public URLs for file
            if (!string.IsNullOrEmpty(response.Image))
                response.Image = fileService.GetPublicURL(response.Image!, new FileDownloadReqOptions(width, height, resize, quality))?.PublicUrl;

            return Ok(response);
        }
    }
}