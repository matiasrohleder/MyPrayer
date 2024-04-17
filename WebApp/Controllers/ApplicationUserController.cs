using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Entities.Constants.Authentication;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tools.WebTools.Attributes;
using WebApp.Models;

namespace WebApp.Controllers;

#region Constructor and properties
[AuthorizeAnyRoles(Roles.Admin, Roles.UserAdmin)]
public class ApplicationUserController(
    IApplicationUserService applicationUserService,
    IService<Content> contentService
    ) : Controller
{
    private readonly IApplicationUserService applicationUserService = applicationUserService;
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
        InitViewDatas("Create");
        return View("CreateOrEdit", new ApplicationUserViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(ApplicationUserViewModel applicationUser)
    {
        if (ModelState.IsValid)
        {
            await applicationUserService.AddAsync(applicationUser.ToEntity(), applicationUser.Roles);
            return RedirectToAction("Index", "ApplicationUser");
        }

        InitViewDatas("Create", applicationUser.Roles);

        return View("CreateOrEdit", applicationUser);
    }
    #endregion

    #region Edit
    public async Task<IActionResult> Edit(Guid id)
    {
        ApplicationUser? applicationuser = await applicationUserService.GetAsync(id);
        if (applicationuser == null)
            return NotFound("Usuario no encontrado");
        ApplicationUserViewModel applicationuserViewModel = new(applicationuser);

        InitViewDatas("Edit");

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

        InitViewDatas("Edit");

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

    #region Private methods
    private void InitViewDatas(string action, IEnumerable<string>? selectedRoles = null)
    {
        ViewData["Action"] = action;
        ViewData["Roles"] = GetRoles(selectedRoles);
    }

    public List<SelectListItem> GetRoles(IEnumerable<string>? selectedRoles = null)
    {
        ICollection<string> roles = Roles.GetAllRoles();

        if (selectedRoles == null)
            return roles.Select(r => new SelectListItem(TranslateRole(r), r)).ToList();

        return roles.Select(r => new SelectListItem(TranslateRole(r), r, selectedRoles.Any(sr => sr == r))).ToList();
    }

    public string TranslateRole(string role)
    {
        return role switch
        {
            Roles.Admin => "Administrador general",
            Roles.CategoryAdmin => "Administrador de categorías",
            Roles.ContentAdmin => "Administrador de contenido",
            Roles.ReadingAdmin => "Administrador de lecturas",
            Roles.UserAdmin => "Administrador de usuarios",
            _ => role,
        };
    }
    #endregion
}