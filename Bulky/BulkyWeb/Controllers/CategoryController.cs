using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _context.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) //Example of custom validation
            {
                ModelState.AddModelError("Name", "Display Order should not match the Name.");
            }

            
			if (obj.Name != null && obj.Name.ToLower() == "test") 
			{
				ModelState.AddModelError("", obj.Name+" is an invalid value.");
			}
			if (ModelState.IsValid)
            {
				_context.Categories.Add(obj);
				_context.SaveChanges();
				return RedirectToAction("Index", "Category");

			}
            return View();
		}

    }
}
