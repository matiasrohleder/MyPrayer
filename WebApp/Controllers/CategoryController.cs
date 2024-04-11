using DataLayer.Interfaces;
using Entities.Constants.Authentication;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tools.WebTools.Attributes;
using WebApp.Models;

namespace WebApp.Controllers;

#region Constructor and properties
[AuthorizeAnyRoles(Roles.Admin, Roles.CategoryAdmin)]
public class CategoryController(
    IService<Category> categoryService,
    IService<Content> contentService
    ) : Controller
{
    private readonly IService<Category> categoryService = categoryService;
    private readonly IService<Content> contentService = contentService;
    #endregion

    #region Index
    public IActionResult Index() => View();
    #endregion

    #region Get all
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<CategoryViewModel> categories = await categoryService.GetAll()
                                            .Select(c => new CategoryViewModel(c))
                                            .ToListAsync();

        return Json(new { data = categories });
    }
    #endregion

    #region Create
    public IActionResult Create()
    {
        ViewData["Action"] = "Create";
        return View("CreateOrEdit", new CategoryViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryViewModel category)
    {
        if (ModelState.IsValid)
        {
            await categoryService.AddAsync(category.ToEntity());
            return RedirectToAction("Index", "Category");
        }

        ViewData["Action"] = "Create";

        return View("CreateOrEdit", category);
    }
    #endregion

    #region Edit
    public async Task<IActionResult> Edit(Guid id)
    {
        Category? category = await categoryService.GetAsync(id);
        if (category == null)
            return NotFound("Categoría no encontrada");
        CategoryViewModel categoryViewModel = new(category);

        ViewData["Action"] = "Edit";

        return View("CreateOrEdit", categoryViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryViewModel category)
    {
        if (ModelState.IsValid)
        {
            await categoryService.UpdateAsync(category.ToEntity());
            return RedirectToAction("Index", "Category");
        }

        ViewData["Action"] = "Edit";

        return View("CreateOrEdit", category);
    }
    #endregion

    #region Delete
    public async Task<JsonResult> Delete(Guid id)
    {
        bool categoryInUse = contentService.GetAll().Any(c => c.CategoryId == id);

        if (!categoryInUse)
        {
            await categoryService.DeleteAsync(id);
            return new JsonResult(null);
        }
        else
            throw new Exception();
    }
    #endregion
}