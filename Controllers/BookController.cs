using BulkyBook.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _db;

        public BookController(ApplicationDBContext db)
        {
            _db = db;
        }

        // GET: Book
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<Book> objCategoryList = _db.Books;
            var books = _db.Books.Include(b => b.Category).ToList();

            return View(books);
        }

        // GET: Book/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(_db.Categories, "Id", "Name");
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(Book book)
        {
            //if (ModelState.IsValid)
            //{
                _db.Books.Add(book);
                _db.SaveChanges();

                TempData["success"] = "Book added successfully ✅";
                return RedirectToAction(nameof(Index));
           // }

            ViewBag.CategoryList = new SelectList(_db.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // GET: Book/Edit/5
        [Authorize]
        public IActionResult Edit(int id)
        {
            var book = _db.Books.Find(id);
            if (book == null) return NotFound();

            ViewBag.CategoryList = new SelectList(_db.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Book book)
        {
            //if (ModelState.IsValid)
            //{
                _db.Books.Update(book);
                _db.SaveChanges();

                TempData["success"] = "Book updated successfully ✏️";
                return RedirectToAction(nameof(Index));
            //}

            ViewBag.CategoryList = new SelectList(_db.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // GET: Book/Delete/5
        [Authorize]
        public IActionResult Delete(int id)
        {
            var book = _db.Books.Find(id);
            if (book == null) return NotFound();

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _db.Books.Find(id);
            if (book == null) return NotFound();

            _db.Books.Remove(book);
            _db.SaveChanges();

            TempData["success"] = "Book deleted successfully ❌";
            return RedirectToAction(nameof(Index));
        }
    }
}
