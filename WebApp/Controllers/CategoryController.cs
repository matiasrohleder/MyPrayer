using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class CategoryController(
    IService<Category> categoryService,
    IService<Content> contentService
    ) : Controller
{
    private readonly IService<Category> categoryService = categoryService;
    private readonly IService<Content> contentService = contentService;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        ViewData["Action"] = "Create";
        return View("CreateOrEdit", new CategoryViewModel());
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<CategoryViewModel> categories = categoryService.GetAll().Where(c => !c.Deleted).Select(c => new CategoryViewModel(c)).ToList();

        return Json(new { data = categories });
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

    public async Task<IActionResult> Edit(Guid id)
    {
        Category category = await categoryService.GetAsync(id);
        CategoryViewModel categoryViewModel = new CategoryViewModel(category);

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
}