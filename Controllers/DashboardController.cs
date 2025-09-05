using Microsoft.AspNetCore.Mvc;
using BulkyBook.Data; // adjust namespace if needed
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDBContext _db;

        public DashboardController(ApplicationDBContext db)
        {
            _db = db;
        }

        // GET: /Dashboard/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Dashboard/Orders
        public IActionResult Orders()
        {
            var orders = _db.Orders
                 //.Include(o => o.Books) // include Book if you want
                 //.Include(o => o.User) // include User if you want
                 .OrderByDescending(o => o.CreatedAt)
                .ToList();

            return View(orders);
        }

        // POST: Approve Order
        public IActionResult Approve(int id)
        {
            var order = _db.Orders.Find(id);
            if (order == null) return NotFound();

            order.Status = "Approved";
            _db.SaveChanges();

            return RedirectToAction("Orders");
        }

        // POST: Reject Order
        public IActionResult Reject(int id)
        {
            var order = _db.Orders.Find(id);
            if (order == null) return NotFound();

            order.Status = "Rejected";
            _db.SaveChanges();

            return RedirectToAction("Orders");
        }

        [HttpPost]
public async Task<IActionResult> UpdateStatus(int id, string status)
{
    var order = await _db.Orders.FindAsync(id);
    if (order == null)
    {
        return NotFound();
    }

    order.Status = status;
    _db.Orders.Update(order);
    await _db.SaveChangesAsync();

    TempData["success"] = "Order status updated successfully!";
    return RedirectToAction("Orders");
}

    }
}
