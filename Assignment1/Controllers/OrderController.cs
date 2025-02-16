using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Assignment1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();
            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        public async Task<IActionResult> Create(Order order, int[] productIds, int[] quantities)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < productIds.Length; i++)
                {
                    var product = await _context.Products.FindAsync(productIds[i]);
                    if (product != null)
                    {
                        order.OrderItems.Add(new OrderItem
                        {
                            ProductId = productIds[i],
                            Quantity = quantities[i]
                        });
                    }
                }

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Products = _context.Products.ToList();
            return View(order);
        }
    }
}