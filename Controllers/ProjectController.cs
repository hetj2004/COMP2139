using COMP2139_Labs.Data;
using COMP2139_Labs.Models;
using comp2139.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Controllers;

public class ProjectController : Controller
{

    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var projects = _context.Projects.ToList();
        return View(projects);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View(); 
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Project project)
    {
        if (ModelState.IsValid)
        {
            _context.Projects.Add(project);      //Add proect to database in memory
            _context.SaveChanges();             //Save changes (persist/commit) to database
            return RedirectToAction("Index");  //Redirect to the  (ProjectController) Index action
        }
        return View(project);
    }
    
    
    [HttpGet]
    public IActionResult Details(int id)
    {
        //Retrieves the project with the ProjectId specified of returns null if not found
        var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
        if (project == null)
        {
            return NotFound();  //Returns a 404 error if the project is not found
        }
        return View(project);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var project = _context.Projects.Find(id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("ProjectId, Name, Description")] Project project)
    {
        if (id != project.ProjectId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Projects.Update(project);  //Update the Project
                _context.SaveChanges();             //Commit changes to database
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!ProjectExists(project.ProjectId))
                {
                    return NotFound();
                }
                else
                {
                    throw;  //throws error if the exception cannot be identified (unknown)
                }
            }
            return RedirectToAction("Index");
        }
        return View(project);
    }

    /// <summary>
    /// Checks if project exists in the database
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool ProjectExists(int id)
    {
        return _context.Projects.Any(e => e.ProjectId == id);
    }

    
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }
    
}