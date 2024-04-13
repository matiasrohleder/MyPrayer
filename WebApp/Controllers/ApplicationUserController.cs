using DataLayer.Interfaces;
using Entities.Constants.Authentication;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tools.WebTools.Attributes;
using WebApp.Models;

namespace WebApp.Controllers;

#region Constructor and properties
[AuthorizeAnyRoles(Roles.Admin, Roles.UserAdmin)]
public class ApplicationUserController(
    IService<ApplicationUser> applicationUserService,
    IService<Content> contentService
    ) : Controller
{
    private readonly IService<ApplicationUser> applicationUserService = applicationUserService;
    private readonly IService<Content> contentService = contentService;
    #endregion

    #region Index
    public IActionResult Index() => View();
    #endregion

    #region Get all
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<ApplicationUserViewModel> applicationUsers = await applicationUserService.GetAll()
                                            .Select(c => new ApplicationUserViewModel(c))
                                            .ToListAsync();

        return Json(new { data = applicationUsers });
    }
    #endregion

    #region Create
    public IActionResult Create()
    {
        ViewData["Action"] = "Create";
        return View("CreateOrEdit", new ApplicationUserViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(ApplicationUserViewModel applicationuser)
    {
        if (ModelState.IsValid)
        {
            await applicationUserService.AddAsync(applicationuser.ToEntity());
            return RedirectToAction("Index", "ApplicationUser");
        }

        ViewData["Action"] = "Create";

        return View("CreateOrEdit", applicationuser);
    }
    #endregion

    #region Edit
    public async Task<IActionResult> Edit(Guid id)
    {
        ApplicationUser? applicationuser = await applicationUserService.GetAsync(id);
        if (applicationuser == null)
            return NotFound("Usuario no encontrado");
        ApplicationUserViewModel applicationuserViewModel = new(applicationuser);

        ViewData["Action"] = "Edit";

        return View("CreateOrEdit", applicationuserViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ApplicationUserViewModel applicationuser)
    {
        if (ModelState.IsValid)
        {
            await applicationUserService.UpdateAsync(applicationuser.ToEntity());
            return RedirectToAction("Index", "ApplicationUser");
        }

        ViewData["Action"] = "Edit";

        return View("CreateOrEdit", applicationuser);
    }
    #endregion

    #region Delete
    public async Task<JsonResult> Delete(Guid id)
    {
        bool userIsMyPrayerAdmin = id == Users.AdminId;

        if (!userIsMyPrayerAdmin)
        {
            await applicationUserService.DeleteAsync(id);
            return new JsonResult(null);
        }
        else
            throw new Exception();
    }
    #endregion
}