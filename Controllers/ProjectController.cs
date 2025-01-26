using COMP2139_Labs.Models;

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
        //Database --> Retrieve project from database
        var projects = new List<Project>()
        {
            new Project { ProjectId = 1, Name = "Project 1", Description = "First Project", }
            //feel free to add more projects here
        }; 
        return View(projects);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
       return View(); 
    }

    [HttpPost]
    public IActionResult Create(Project project)
    {
        //Database --> Retrieve project from database
        return RedirectToAction("Index");
    }
    
    //CRUD - Create - Read - Update - Delete
    
    [HttpGet]

    public IActionResult Details(int id)
    {
        //Database --> Retrieve project from database
        var project = new Project { ProjectId = id, Name = "Project" + id, Description = "Details of Project" + id};
        return View(project);
    }
    

}