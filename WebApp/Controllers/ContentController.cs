using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Entities.Constants.Authentication;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tools.WebTools.Attributes;
using WebApp.Models;

namespace WebApp.Controllers;

#region Contructor and properties
[AuthorizeAnyRoles(Roles.Admin, Roles.ContentAdmin)]
public class ContentController(
    IService<Category> categoryService,
    IService<Content> contentService,
    IFileService fileService
    ) : Controller
{
    private readonly IService<Category> categoryService = categoryService;
    private readonly IService<Content> contentService = contentService;
    private readonly IFileService fileService = fileService;
    #endregion

    #region Index
    public IActionResult Index()
    {
        return View();
    }
    #endregion

    #region Get all
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<ContentRow> contents = await contentService.GetAll()
                                    .Include(c => c.Category)
                                    .Select(c => new ContentRow(c))
                                    .ToListAsync();

        return Json(new { data = contents });
    }
    #endregion

    #region Create
    public async Task<IActionResult> Create()
    {
        await InitViewDatas("Create");
        
        return View("CreateOrEdit", new ContentViewModel());
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

        await GetContentSignedURLAsync(content);

        return View("CreateOrEdit", content);
    }
    #endregion

    #region Edit
    public async Task<IActionResult> Edit(Guid id)
    {
        Content? content = await contentService.GetAsync(id);
        if (content == null)
            return NotFound("Contenido no encontrado");
            
        ContentViewModel contentViewModel = new(content);
        
        await GetContentSignedURLAsync(contentViewModel);

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
        
        await GetContentSignedURLAsync(content);

        await InitViewDatas("Edit");

        return View("CreateOrEdit", content);
    }
    #endregion

    #region Delete
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
    #endregion

    #region Private methods
    private async Task InitViewDatas(string action)
    {
        ViewData["Action"] = action;
        ViewData["Categories"] = await categoryService.GetAll().Where(c => c.Active && !c.Deleted).OrderBy(c => c.Order).Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToListAsync();
    }

    private async Task GetContentSignedURLAsync(ContentViewModel contentViewModel)
    {
        if(!string.IsNullOrEmpty(contentViewModel.FileUrl))
            contentViewModel.SignedUrl = (await fileService.GetSignedURLAsync(contentViewModel.FileUrl))?.SignedUrl;
    }
    #endregion
}