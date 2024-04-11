using DataLayer.Interfaces;
using Entities.Constants.Authentication;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tools.WebTools.Attributes;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    [AuthorizeAnyRoles(Roles.Admin, Roles.CategoryAdmin)]
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
