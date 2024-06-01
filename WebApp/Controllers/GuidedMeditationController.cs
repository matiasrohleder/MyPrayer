using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataLayer.Interfaces;
using Entities.Constants.Authentication;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tools.WebTools.Attributes;
using WebApp.Models;

namespace WebApp.Controllers;

#region Contructor and properties
[AuthorizeAnyRoles(Roles.Admin, Roles.GuidedMeditationAdmin)]
public class GuidedMeditationController(
    IGuidedMeditationBusinessLogic guidedMeditationBusinessLogic,
    IService<GuidedMeditation> guidedMeditationService,
    IFileService fileService
    ) : Controller
{
    private readonly IGuidedMeditationBusinessLogic guidedMeditationBusinessLogic = guidedMeditationBusinessLogic;
    private readonly IService<GuidedMeditation> guidedMeditationService = guidedMeditationService;
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
        List<GuidedMeditationRow> guidedMeditations = await guidedMeditationService.GetAll()
                                                                                   .Select(gm => new GuidedMeditationRow(gm))
                                                                                   .ToListAsync();

        return Json(new { data = guidedMeditations });
    }
    #endregion

    #region Create
    public async Task<IActionResult> Create()
    {
        InitViewDatas("Create");
        
        return View("CreateOrEdit", new GuidedMeditationViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(GuidedMeditationViewModel guidedMeditationViewModel)
    {
        GuidedMeditation guidedMeditation = guidedMeditationViewModel.ToEntity();
        await guidedMeditationBusinessLogic.ValidateGuidedMeditation(guidedMeditation, ModelState);

        if (ModelState.IsValid)
        {
            await guidedMeditationService.AddAsync(guidedMeditation);
            return RedirectToAction("Index", "GuidedMeditation");
        }

        InitViewDatas("Create");

        GetContentPublicURL(guidedMeditationViewModel);

        return View("CreateOrEdit", guidedMeditationViewModel);
    }
    #endregion

    #region Edit
    public async Task<IActionResult> Edit(Guid id)
    {
        GuidedMeditation? guidedMeditation = await guidedMeditationService.GetAsync(id);
        if (guidedMeditation == null)
            return NotFound("Meditación guiada no encontrada");
            
        GuidedMeditationViewModel guidedMeditationViewModel = new(guidedMeditation);
        
        GetContentPublicURL(guidedMeditationViewModel);

        InitViewDatas("Edit");

        return View("CreateOrEdit", guidedMeditationViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(GuidedMeditationViewModel guidedMeditationViewModel)
    {
        GuidedMeditation guidedMeditation = guidedMeditationViewModel.ToEntity();
        await guidedMeditationBusinessLogic.ValidateGuidedMeditation(guidedMeditation, ModelState);

        if (ModelState.IsValid)
        {
            await guidedMeditationService.UpdateAsync(guidedMeditation);
            return RedirectToAction("Index", "GuidedMeditation");
        }
        
        GetContentPublicURL(guidedMeditationViewModel);

        InitViewDatas("Edit");

        return View("CreateOrEdit", guidedMeditationViewModel);
    }
    #endregion

    #region Delete
    public async Task<JsonResult> Delete(Guid id)
    {
        try
        {
            await guidedMeditationService.DeleteAsync(id);
            return new JsonResult(null);
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Private methods
    private void InitViewDatas(string action)
    {
        ViewData["Action"] = action;
    }

    private void GetContentPublicURL(GuidedMeditationViewModel guidedMeditationViewModel, FileDownloadReqOptions? options = null)
    {
        if(!string.IsNullOrEmpty(guidedMeditationViewModel.FileUrl))
            guidedMeditationViewModel.SignedUrl = fileService.GetPublicURL(guidedMeditationViewModel.FileUrl)?.SignedUrl;
    }
    #endregion
}