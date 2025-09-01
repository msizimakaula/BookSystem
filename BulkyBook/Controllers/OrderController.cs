using BulkyBook.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDBContext _db;

        public OrderController(ApplicationDBContext db) //db has implementation of all connection strings and tables 
        {
            _db = db;
        }
        public IActionResult Create()
        {
            // Populate dropdown with categories from the database
            ViewBag.Categories = _db.Categories
                .Where(c => c.Status == "Available") // optional filter for available categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _db.Orders.Add(order);
                await _db.SaveChangesAsync();
                return RedirectToAction("Success"); // Redirect to a success page
            }

            // If model is invalid, repopulate categories
            ViewBag.Categories = _db.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            return View(order);
        }

    //    // Optional: Success page
    //    public IActionResult Success()
    //    //{
    //    //    return View();
    //    //}
    }
}
