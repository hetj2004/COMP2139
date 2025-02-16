using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartInventoryManagement.Data;
using SmartInventoryManagement.Models;

public class SearchController : Controller
{
    private readonly AppDbContext _context;

    public SearchController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string query)
    {
        var products = string.IsNullOrEmpty(query)
            ? _context.Products.Include(p => p.Category).ToList()
            : _context.Products.Include(p => p.Category)
                .Where(p => p.Name.Contains(query))
                .ToList();

        return View(products);
    }
}