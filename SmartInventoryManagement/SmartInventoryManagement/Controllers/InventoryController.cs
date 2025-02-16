using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartInventoryManagement.Data;
using SmartInventoryManagement.Models;

public class InventoryController : Controller
{
    private readonly AppDbContext _context;
    public InventoryController(AppDbContext context) => _context = context;

    public IActionResult Index()
    {
        var products = _context.Products
            .Include(p => p.Category)
            .AsNoTracking()
            .ToList();

        return View(products);
    }



    public IActionResult Add()
    {
        var categories = _context.Categories.ToList();
        ViewBag.Categories = categories;

        return View();
    }


    [HttpPost]
    public IActionResult Add(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Categories = _context.Categories.ToList();
        return View(product);
    }


    [HttpGet]
    public IActionResult Edit(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        // Populate categories for the dropdown
        ViewBag.Categories = _context.Categories.ToList();
    
        return View(product);
    }


    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(product);
    }

    public IActionResult Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}