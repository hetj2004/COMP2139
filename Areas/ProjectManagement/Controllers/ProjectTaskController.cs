using COMP2139_Labs.Data;
using COMP2139_Labs.Models;
using COMP2139_Labs.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Areas.ProjectManagement.Controllers;

[Area("ProjectManagement")]
[Route("[area]/[controller]/[action]")]
public class ProjectTaskController : Controller
{
    
    private readonly ApplicationDbContext _context;

    public ProjectTaskController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("{projectId}")]
    public async Task<IActionResult> Index(int projectId)
    {
        var tasks = await _context.Tasks
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
        
        ViewBag.ProjectId = projectId; 
        return View(tasks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var task = await _context
            .Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t =>t.ProjectId == id);

        if (task == null)
        {
            return NotFound();
        }
        return View(task);
    }
    
    [HttpGet("Create/{projectId:int}")]
    public IActionResult Create(int projectId)
    {
        var project = _context.Projects.Find(projectId);
        if (project == null)
        {
            return NotFound();
        }

        var task = new ProjectTask
        {
            ProjectId = projectId,
            Title = "",
            Description = "",
        };
        return View(task);
    }

    [HttpPost("Create/{projectId:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title", "Description", "ProjectId")]ProjectTask task)
    {
        if (ModelState.IsValid)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { projectId = task.ProjectId });
        }
        return View(task);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var task = await _context
            .Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t =>t.ProjectId == id);

        if (task == null)
        {
            return NotFound();
        }
        return View(task);
    }

    [HttpPost("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProjectTaskId", "Title", "Description", "ProjectId")] ProjectTask task)
    {
        if (id != task.ProjectTaskId)
        {
            return NotFound();
        }
        
        if (ModelState.IsValid)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { projectId = task.ProjectId });
        }
        return View(task);
        
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _context
            .Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t =>t.ProjectTaskId == id);

        if (task == null)
        {
            return NotFound();
        }
        return View(task);
    }

    [HttpPost("{ProjectTaskId:int}"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int ProjectTaskId)
    {
        var task = await _context.Tasks.FindAsync(ProjectTaskId);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChangesAsync();
            return RedirectToAction("Index", new { projectId = task.ProjectId });
        }
        return View(task);
    }
    
    [HttpGet("Search")]
    public async Task<IActionResult> Search(int? projectId, string searchString)
    {
        var tasksQuery = _context.Tasks.AsQueryable();

        bool searchPerformed = !string.IsNullOrWhiteSpace(searchString);

        if (projectId.HasValue)
        {
            tasksQuery = tasksQuery.Where(t => t.ProjectTaskId == projectId);
        }

        if (searchPerformed)
        {
            searchString = searchString.ToLower();

            tasksQuery = tasksQuery.Where(p => p.Title.ToLower().Contains(searchString)
                                                     || p.Description.ToLower().Contains(searchString));
        }

        var tasks = await tasksQuery.ToListAsync();
        
        ViewBag.ProjectId = projectId;
        ViewData["SearchString"] = searchString;
        ViewData["SearchPerformed"] = searchPerformed;
        
        return View("Index", tasks);
    }

    
}