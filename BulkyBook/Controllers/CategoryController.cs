using BulkyBook.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db;

        public CategoryController(ApplicationDBContext db) //db has implementation of all connection strings and tables 
        {
            _db = db;       
        }
        public IActionResult Index(string searchString)
        {
            IEnumerable<Category> objCategoryList = _db.Categories; //retrieve all categs froM table DB

            if (!string.IsNullOrEmpty(searchString))
            {
                objCategoryList = objCategoryList
                    .Where(c => c.Name.ToLower().Contains(searchString.ToLower())); // case-insensitive partial search
            

        }

            ViewData["CurrentFilter"] = searchString; // keeps search text in input

            // Check if nothing found
            if (!string.IsNullOrEmpty(searchString) && !objCategoryList.Any())
            {
                ViewData["NoResults"] = $"No categories found for: {searchString}";
            }
            else if (!string.IsNullOrEmpty(searchString))
            {
                ViewData["SearchResults"] = $"Showing results for: {searchString}";
            }

            return View(objCategoryList); //IEnu.. to view
        }

        //GET
        public IActionResult Create() 
        {
            
            return View(); 
        }

        //post there incl hhtp post
        [HttpPost]
        [ValidateAntiForgeryToken] //helps & prevents cross-site request forgery attacck
        public IActionResult Create(Category obj) 
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid) // checks model validation (required etc)
            {
                _db.Categories.Add(obj); //adds to database
                _db.SaveChanges(); //pushes to DB
                TempData["success"] = "Catgeory added successfully";
                return RedirectToAction("Index"); //redirects to index action in same controller 
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            //3 ways to retroieve a category
            var categFromDb = _db.Categories.Find(id);
            //var categFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id); //
            //var categFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if(categFromDb == null)
            {
                return NotFound();
            }
            return View(categFromDb);
        }

        //post there incl hhtp post
        [HttpPost]
        [ValidateAntiForgeryToken] //helps & prevents cross-site request forgery attacck
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid) // checks model validation (required etc)
            {
                _db.Categories.Update(obj); //adds to database
                _db.SaveChanges(); //pushes to DB
                TempData["success"] = "Catgeory updated successfully";
                return RedirectToAction("Index"); //redirects to index action in same controller 
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //3 ways to retroieve a category
            var categFromDb = _db.Categories.Find(id);
            //var categFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id); //
            //var categFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categFromDb == null)
            {
                return NotFound();
            }
            return View(categFromDb);
        }

        //post there incl hhtp post
        [HttpPost, ActionName("Delete")] //focully naming the DeletePOST to Delete
        [ValidateAntiForgeryToken] //helps & prevents cross-site request forgery attacck
        public IActionResult DeleteP(int? id) //receives id
        {
            var obj = _db.Categories.Find(id); //retrieve categ
            if (obj == null)
            {
                return NotFound();
            }
                
          _db.Categories.Remove(obj); //adds to database
                _db.SaveChanges(); //pushes to DB
            TempData["success"] = "Catgeory deleted successfully";
            return RedirectToAction("Index"); //redirects to index action in same controller 
            
            
        }



    }
}
