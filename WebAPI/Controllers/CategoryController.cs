using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IService<Category> categoryService;

        public CategoryController(IService<Category> categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAllActive()
        {
            List<CategoryItem> categories = categoryService.GetAll()
                                                           .Where(c => !c.Deleted && c.Active)
                                                           .OrderBy(c => c.Order)
                                                           .Select(c => new CategoryItem(c))
                                                           .ToList();

            return Ok(categories);
        }
    }
}
