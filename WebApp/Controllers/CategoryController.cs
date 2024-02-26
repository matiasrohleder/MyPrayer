using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class CategoryController(
    IService<Category> categoryService
    ) : Controller
{
    private readonly IService<Category> categoryService = categoryService;

    public IActionResult Index()
    {
        return View();
    }

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
}