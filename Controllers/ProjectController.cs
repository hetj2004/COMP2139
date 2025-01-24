using comp2139.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP2139_Labs.Controllers;

public class ProjectController : Controller
{
    /// <summary>
    /// Index action will retrieve a listing of projects (database)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Index()
    {
        var projects = new List<Project>
        {
            new Project { ProjectId = 1, Name = "Project 1", Description = "First Project 1" },
            // Feel free to define more projects
        };
        return View(projects);
    }
}