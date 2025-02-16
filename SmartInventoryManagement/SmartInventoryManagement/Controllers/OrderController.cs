using Microsoft.AspNetCore.Mvc;
using SmartInventoryManagement.Data;
using SmartInventoryManagement.Models;
using System.Linq;

public class OrderController : Controller
{
    private readonly AppDbContext _context;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Order order)
    {
        if (ModelState.IsValid)
        {
            order.OrderDate = DateTime.Now;
            order.TotalPrice = CalculateTotal(order.OrderProducts);
            _context.Orders.Add(order);
            _context.SaveChanges();
            return RedirectToAction("Confirmation", new { id = order.Id });
        }
        return View(order);
    }

    public IActionResult Confirmation(int id)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == id);
        return View(order);
    }

    private decimal CalculateTotal(List<OrderProduct> products) =>
        products.Sum(p => p.Product.Price * p.Quantity);
}