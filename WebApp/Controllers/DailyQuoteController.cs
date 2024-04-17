using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Entities.Constants.Authentication;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tools.WebTools.Attributes;
using WebApp.Models;

namespace WebApp.Controllers;

#region Constructor and properties
[AuthorizeAnyRoles(Roles.Admin, Roles.DailyQuoteAdmin)]
public class DailyQuoteController(
    IDailyQuoteBusinessLogic dailyQuoteBusinessLogic,
    IService<DailyQuote> dailyQuoteService
    ) : Controller
{
    private readonly IDailyQuoteBusinessLogic dailyQuoteBusinessLogic = dailyQuoteBusinessLogic;
    private readonly IService<DailyQuote> dailyQuoteService = dailyQuoteService;
    #endregion

    #region Index
    public IActionResult Index() => View();
    #endregion

    #region Get all
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<DailyQuoteRow> quotes = await dailyQuoteService.GetAll()
                                            .OrderByDescending(d => d.Date)
                                            .Select(c => new DailyQuoteRow(c))
                                            .ToListAsync();

        return Json(new { data = quotes });
    }
    #endregion

    #region Create
    public IActionResult Create()
    {
        ViewData["Action"] = "Create";
        return View("CreateOrEdit", new DailyQuoteViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(DailyQuoteViewModel dailyQuoteViewModel)
    {
        DailyQuote dailyQuote = dailyQuoteViewModel.ToEntity();
        await dailyQuoteBusinessLogic.ValidateQuote(dailyQuote, ModelState);

        if (ModelState.IsValid)
        {
            await dailyQuoteService.AddAsync(dailyQuote);
            return RedirectToAction("Index", "DailyQuote");
        }

        ViewData["Action"] = "Create";

        return View("CreateOrEdit", dailyQuoteViewModel);
    }
    #endregion

    #region Edit
    public async Task<IActionResult> Edit(Guid id)
    {
        DailyQuote? dailyQuote = await dailyQuoteService.GetAsync(id);
        if (dailyQuote == null)
            return NotFound("Frase diaria no encontrada");
        DailyQuoteViewModel dailyQuoteViewModel = new(dailyQuote);

        ViewData["Action"] = "Edit";

        return View("CreateOrEdit", dailyQuoteViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DailyQuoteViewModel dailyQuoteViewModel)
    {
        DailyQuote dailyQuote = dailyQuoteViewModel.ToEntity();
        await dailyQuoteBusinessLogic.ValidateQuote(dailyQuote, ModelState);

        if (ModelState.IsValid)
        {
            await dailyQuoteService.UpdateAsync(dailyQuote);
            return RedirectToAction("Index", "DailyQuote");
        }

        ViewData["Action"] = "Edit";

        return View("CreateOrEdit", dailyQuoteViewModel);
    }
    #endregion

    #region Delete
    public async Task<JsonResult> Delete(Guid id)
    {
        await dailyQuoteService.DeleteAsync(id);
        return new JsonResult(null);
    }
    #endregion
}