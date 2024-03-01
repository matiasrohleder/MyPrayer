using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers;

public class ContentController(
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

    public async Task<IActionResult> Create()
    {
        await InitViewDatas("Create");

        return View("CreateOrEdit", new ContentViewModel());
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<ContentRow> contents = contentService.GetAll().Include(c => c.Category).Where(c => !c.Deleted).Select(c => new ContentRow(c)).ToList();

        return Json(new { data = contents });
    }

    [HttpPost]
    public async Task<IActionResult> Create(ContentViewModel content)
    {
        if (ModelState.IsValid)
        {
            await contentService.AddAsync(content.ToEntity());
            return RedirectToAction("Index", "Content");
        }

        await InitViewDatas("Create");

        return View("CreateOrEdit", content);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        Content content = await contentService.GetAsync(id);
        ContentViewModel contentViewModel = new ContentViewModel(content);

        await InitViewDatas("Edit");

        return View("CreateOrEdit", contentViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ContentViewModel content)
    {
        if (ModelState.IsValid)
        {
            await contentService.UpdateAsync(content.ToEntity());
            return RedirectToAction("Index", "Content");
        }

        await InitViewDatas("Edit");

        return View("CreateOrEdit", content);
    }

    public async Task<JsonResult> Delete(Guid id)
    {
        try
        {
            await contentService.DeleteAsync(id);
            return new JsonResult(null);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task InitViewDatas(string action)
    {
        ViewData["Action"] = action;
        ViewData["Categories"] = await categoryService.GetAll().Where(c => c.Active && !c.Deleted).OrderBy(c => c.Order).Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToListAsync();
    }
}