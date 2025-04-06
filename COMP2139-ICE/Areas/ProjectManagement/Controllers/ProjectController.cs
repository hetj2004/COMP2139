using COMP2139_ICE.Data;
using COMP2139_ICE.Models;
using COMP2139_ICE.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_ICE.Areas.ProjectManagement.Controllers;

[Area("ProjectManagement")]
[Route("[area]/[controller]/[action]")]
public class ProjectController : Controller
{
    private readonly ILogger<ProjectController> _logger;
    private readonly ApplicationDbContext _context;
    public ProjectController(ApplicationDbContext context, ILogger<ProjectController> logger)
    {
        _logger = logger;
        _context = context;    
    }
    
    /// <summary>
    /// Index action will retrieve a listing of projects (database)
    /// </summary>
    /// <returns></returns>
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Accessed ProjectController Index at {Time}", DateTime.Now);
        // Database --> Retrieve all projects from database
        var projects = await _context.Projects.ToListAsync();
        return View(projects);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        _logger.LogInformation("Accessed ProjectController Create at {Time}", DateTime.Now);
        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Project project)
    {
        // Database --> Persist new project to the database
        if (ModelState.IsValid)
        {
            _context.Projects.Add(project); // Add new project to database
            await _context.SaveChangesAsync();         // Save changes to database
            return RedirectToAction("Index");
        }
        return View(project);
    }
    
    // CRUD: Create - Read - Update - Delete

    [HttpGet("Details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        _logger.LogInformation("Accessed ProjectController Details at {Time}", DateTime.Now);
        
        // Database --> Retrieve the project with the specified id from database or return null if not found
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
        if (project == null)
        {
            _logger.LogWarning("Could not find  Project with ID of {id}", id);
            return NotFound();
        }
        return View(project);
    }
    
    [HttpGet("Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        // Database --> Retrieve the project with the specified id from database or return null if not found
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProjectId, Name, Description")] Project project)
    {
        // [Bind] ensure only the specified properties are updated
        if (id != project.ProjectId)
        {
            return NotFound(); // ensures the in the route matches the ID model
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Projects.Update(project); // Update the project
                await _context.SaveChangesAsync();            // Commit the changes
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProjectExists(project.ProjectId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        } 
        return View(project);
    }

    private async Task<bool> ProjectExists(int id)
    {
        return await _context.Projects.AnyAsync(e => e.ProjectId == id);
    }
    
    [HttpGet("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        // Database --> Retrieve the project with the specified id from database or return null if not found
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpPost("Delete/{ProjectId:int}"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int ProjectId)
    {
        var project = await _context.Projects.FindAsync(ProjectId);
        if (project != null)
        {
            _context.Projects.Remove(project);  // Remove project from database
            await _context.SaveChangesAsync();             // Commit changes to database
            return RedirectToAction("Index");
        }
        return View(project);
    }
    
    [HttpGet("Search/{searchString}")]
    public async Task<IActionResult> Search(string searchString)
    {
        var projectsQuery = _context.Projects.AsQueryable();
        
        bool searchPerformed = !string.IsNullOrEmpty(searchString);

        if (searchPerformed)
        {
            searchString = searchString.ToLower();
            projectsQuery = projectsQuery.Where(p => p.Name.ToLower().Contains(searchString) || 
                                                     p.Description.ToLower().Contains(searchString));
        }

        // Asynchronous execution means this method does not block the thread while waiting for the database 
        var projects = await projectsQuery.ToListAsync();
        
        // Pass search metadata
        ViewData["SearchString"] = searchString;
        ViewData["SearchPerformed"] = searchPerformed;
        
        return View("Index", projects);
    }
}