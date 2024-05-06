using BusinessLayer.BusinessLogic;
using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Entities.Models;
using Entities.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Web.Mvc.Html;
using WebApp.Models;

namespace WebApp.Controllers;

#region Constructor and properties
public class ReadingController(
    IReadingBusinessLogic readingBusinessLogic,
    IService<Reading> readingService
    ) : Controller
{
    private readonly IReadingBusinessLogic readingBusinessLogic = readingBusinessLogic;
    private readonly IService<Reading> readingService = readingService;
    #endregion

    #region Index
    public IActionResult Index() => View();
    #endregion

    #region Get all
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<ReadingRow> readings = await readingService.GetAll()
                                            .Select(r => new ReadingRow(r))
                                            .ToListAsync();

        return Json(new { data = readings });
    }
    #endregion

    #region Create
    public async Task<IActionResult> Create()
    {
        await InitViewDatas("Create");
        return View("CreateOrEdit", new ReadingViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(ReadingViewModel readingViewModel)
    {
        Reading reading = readingViewModel.ToEntity();
        await readingBusinessLogic.ValidateReading(reading, ModelState);

        if (ModelState.IsValid)
        {
            await readingService.AddAsync(reading);
            return RedirectToAction("Index", "Reading");
        }

        await InitViewDatas("Create");

        return View("CreateOrEdit", readingViewModel);
    }
    #endregion

    #region Edit
    public async Task<IActionResult> Edit(Guid id)
    {
        Reading? reading = await readingService.GetAsync(id);
        if (reading == null)
            return NotFound("Lectura no encontrada");
        ReadingViewModel readingViewModel = new(reading);

        await InitViewDatas("Edit");

        return View("CreateOrEdit", readingViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ReadingViewModel readingViewModel)
    {
        Reading reading = readingViewModel.ToEntity();
        await readingBusinessLogic.ValidateReading(reading, ModelState);

        if (ModelState.IsValid)
        {
            await readingService.UpdateAsync(reading);
            return RedirectToAction("Index", "Reading");
        }

        await InitViewDatas("Edit");

        return View("CreateOrEdit", readingViewModel);
    }
    #endregion

    #region Private methods
    private async Task InitViewDatas(string action)
    {
        ViewData["Action"] = action;

        ViewData["Types"] = EnumHelper.GetSelectList(typeof(ReadingEnum)).Select(e => new SelectListItem
        {
            Text = e.Text,
            Value = e.Value
        }).ToList();
    }
    #endregion
}