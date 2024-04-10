using System.Diagnostics;
using Entities.Constants.Authentication;
using Microsoft.AspNetCore.Mvc;
using Tools.WebTools.Attributes;
using WebApp.Models;

namespace WebApp.Controllers;

[AuthorizeAnyRoles(Roles.Admin)]
public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
