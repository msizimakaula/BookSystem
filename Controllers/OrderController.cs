using BulkyBook.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Net.Mail;

namespace BulkyBook.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;

        public OrderController(ApplicationDBContext db, IConfiguration configuration, UserManager<Users> userManager)
        {
            _db = db;
            _configuration = configuration;
            _userManager = userManager;
        }

        [Authorize]
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
                order.Status = "Pending";         // default status
                order.CreatedAt = DateTime.Now;   // set timestamp
                _db.Orders.Add(order);
                await _db.SaveChangesAsync();
                // Get the logged-in user's email
                var user = await _userManager.GetUserAsync(User);
            //    var userEmail = user?.Email ?? order.Email;

            //    // Send email
            //    SendEmail(userEmail, order);

                //TempData["OrderSuccess"] = "Your order has been placed successfully! You will receive an email with collection details.";

                return RedirectToAction("Index", "Home"); // Redirect to a success page
            }

            // If model is invalid, repopulate categories
            ViewBag.Categories = _db.Categories
                 .Where(c => c.Status == "Available")
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            return View(order);

        }


        private void SendEmail(string email, Order order)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");
            var smtpClient = new SmtpClient(emailSettings["SmtpServer"])
            {
                Port = int.Parse(emailSettings["SmtpPort"]),
                Credentials = new NetworkCredential(emailSettings["SenderEmail"], emailSettings["Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings["SenderEmail"], emailSettings["SenderName"]),
                Subject = "Your Book Order Details",
                Body = $"Hi {order.Name},\n\nThank you for your order! Your book will be ready for collection soon.\n\nOrder Details:\n- Name: {order.Name} {order.Surname}\n- Category: {order.CategoryId}\n\nRegards,\nBook Store",
                IsBodyHtml = false,
            };
            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);
        }

        //    // Optional: Success page
        //    public IActionResult Success()
        //    //{
        //    //    return View();
        //    //}
    }
}
