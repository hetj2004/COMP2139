using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using COMP2139_Labs.Models;

namespace COMP2139_Labs.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult GeneralSearch(string searchType, string searchString)
    {
        if (searchType == "projects")
        {
            return RedirectToAction("Search", "Project", new { area ="ProjectManagement",searchString });
        }

        else if (searchType == "Tasks")
        {
            return RedirectToAction("Search", "ProjectTask", new { area ="ProjectManagement",searchString });
        }
        return RedirectToAction(nameof(Index), "Home");
    }
}