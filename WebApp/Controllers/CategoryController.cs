using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

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
}