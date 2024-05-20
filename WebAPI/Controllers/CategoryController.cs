using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IService<Category> categoryService;

        public CategoryController(IService<Category> categoryService)
        {
            this.categoryService = categoryService;
        }

        /// <summary>
        /// Get all active categories in order
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<CategoryRes>> GetAllActive()
        {
            List<CategoryRes> categories = categoryService.GetAll()
                                                           .Where(c => !c.Deleted && c.Active)
                                                           .OrderBy(c => c.Order)
                                                           .Select(c => new CategoryRes(c))
                                                           .ToList();

            return Ok(categories);
        }
    }
}
